using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DialogSystem;
using DialogSystem.Logic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using XNode;

public class GraphReader : MonoBehaviour
{
    public SceneGraph sceneGraph;
    public Node currentNode;
    public TextMeshProUGUI  textMesh;
    public TextMeshProUGUI  nameText;
    public Image characterSprite;
    public GameObject answersList;
    public GameObject answerPrefab;
    public Root rootNode;
    public Camera camera;
    [SerializeField]
    private AudioSource _dialogueAudioSource;
    [SerializeField] private GameObject player;

    public delegate void FocusOnObject(GameObject gameObject);

    
    /// <summary>
    /// This needs to be set, otherwise it will throw the Not Implemented Exception
    /// </summary>
    [SerializeField]
    public FocusOnObject FocusOnObjectAction = o => throw new NotImplementedException();
    
    /*[SerializeField]
    private AnimationCurve focusCurve;
    [SerializeField]
    private float focusSpeed=0.03f;*/

    public float textSpeed =0.025f;
    // Start is called before the first frame update
    
    /// <summary>
    /// Loads up the dialog by reading the root node
    /// </summary> 
    void Start()
    {
        rootNode = sceneGraph.graph.nodes.Find(x => x is Root) as Root;
        currentNode= rootNode;
        /*var outputs = currentNode.Outputs;
        if (outputs.Count() == 1)
        {
            currentNode = outputs.ToArray()[0].GetConnection(0).node;
            Debug.Log(currentNode.GetType());
        }*/
        NextChoice(0);
    }

    /// <summary>
    /// Draws next choice based on the chosen answer  
    /// </summary>
    /// <param name="choice">index of choice</param>
    public void NextChoice(int choice)
    {
        try
        {
            StopCoroutine("TextFadeIn");
        }
        catch (Exception e)
        {
            Debug.Log("Text wasn't skipped");
        }
        Type type;
        var node = NextNode(out type, choice);
        if (type == typeof(TextDialogNode))
        {
            TextDialogNode textDialog = node as TextDialogNode;
            StartCoroutine(TextFadeIn(textDialog.dialogText));
            //TextMesh.text = textDialog.dialogText;
            nameText.text = textDialog.characterName;
            characterSprite.sprite = textDialog.sprite;
            _dialogueAudioSource.clip = textDialog.audioClip;
            _dialogueAudioSource.Play();
            RegenerateChoices(new List<(string, int)> { ("Next", 0) });
        }
        else if (type == typeof(DecisionDialogNode))
        {
            DecisionDialogNode decisionDialog = node as DecisionDialogNode;
            StartCoroutine(TextFadeIn(decisionDialog.dialogText));
            //TextMesh.text = decisionDialog.dialogText;
            nameText.text = decisionDialog.characterName;
            _dialogueAudioSource.clip = decisionDialog.audioClip;
            _dialogueAudioSource.Play();
            characterSprite.sprite = decisionDialog.sprite;
            RegenerateChoices(decisionDialog.GetChoices());
        }
        else if(type== typeof(FocusNode))
        {
            Debug.LogWarning("FOOOOCUS NOOOODE");
            var focusNode = node as FocusNode;
            FocusOnObjectAction(focusNode.focusObject);
            NextChoice(0);
            //camera.transform.rotation.R
        }
        
    }
    private float Lerp(float var1, float var2, float time)
    {
        var difference = var2 - var1;
        return var1 + difference * time;
    }
    
    public void RegenerateChoices(List<(string, int)> choices)
    {
        int i = 0;
        foreach(Transform child in answersList.transform)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("Cleared choices");
        foreach (var choice in choices)
        {
            GameObject answer= Instantiate(answerPrefab, answersList.transform);
            answer.GetComponentInChildren<TextMeshProUGUI>().text = choice.Item1;
            answer.GetComponent<Button>().onClick.AddListener(()=>
            {
                NextChoice(choice.Item2);
                Debug.Log($"CHOSEN : {choice.Item2}");
            });
        }
    }
    
    /// <summary>
    /// Returns the reference to the next node
    /// </summary>
    /// <param name="nodeType">Type of the node returned</param>
    /// <param name="index">Which option was selected, default is the first one (0)</param>
    /// TODO: Rewrite to use tuple for NodeType
    Node NextNode(out Type nodeType, int index = 0)
    {
        //var outputs = currentNode.Outputs;
        //currentNode = outputs.ToArray()[index].GetConnection(0).node;
        Debug.Log(currentNode.GetType());
        if (IsCalculable(currentNode))
        {
            var we = currentNode as ITraversable;
            currentNode =we.NextNode(index);
            //Debug.Log(we);
            //TODO calculate the node and return next node requiring user input
        }
        else
        {
            var we = currentNode as ITraversable;
            currentNode =we.NextNode(index);
            //Debug.Log(we);
        }
        nodeType = currentNode.GetType();
        return currentNode;
    }
    /// <summary>
    /// Returns true if the node is a calculable node and false if node is requiring user input or decision 
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    bool IsCalculable(Node node)
    {
        return node is OrNode || node is AndNode || node is Not;
    }

    /// <summary>
    /// Simple text writing animation
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    IEnumerator TextFadeIn(string text)
    {
        int x = 0;
        while (x < text.Length)
        {
            x++;
            textMesh.text = text.Substring(0, x);
            yield return new WaitForSeconds(textSpeed);
        }
        _dialogueAudioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
