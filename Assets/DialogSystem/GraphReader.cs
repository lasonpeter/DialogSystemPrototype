using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DialogSystem;
using DialogSystem.Logic;
using TMPro;
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
    public Dictionary<string, GameObject> GameObjectsReferences = new();

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
        StopAllCoroutines();
        Type type;
        var node = NextNode(out type, choice);
        if (type == typeof(TextDialogNode))
        {
            TextDialogNode textDialog = node as TextDialogNode;
            StartCoroutine(TextFadeIn(textDialog.dialogText));
            //TextMesh.text = textDialog.dialogText;
            nameText.text = textDialog.characterName;
            characterSprite.sprite = textDialog.sprite;
            RegenerateChoices(new List<(string, int)> { ("Next", 0) });
        }
        else if (type == typeof(DecisionDialogNode))
        {
            DecisionDialogNode decisionDialog = node as DecisionDialogNode;
            StartCoroutine(TextFadeIn(decisionDialog.dialogText));
            //TextMesh.text = decisionDialog.dialogText;
            nameText.text = decisionDialog.characterName;
            characterSprite.sprite = decisionDialog.sprite;
            RegenerateChoices(decisionDialog.GetChoices());
        }
        
    }


    /// <summary>
    /// Redraws the buttons that control the choices
    /// </summary>
    /// <param name="choices">List of choice tuples</param>
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

    IEnumerator TextFadeIn(string text)
    {
        int x = 0;
        while (x < text.Length)
        {
            x++;
            textMesh.text = text.Substring(0, x);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
