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
    public TextMeshProUGUI  TextMesh;
    public GameObject answersList;
    public GameObject answerPrefab;
    public Root rootNode;
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

    public void NextChoice(int choice)
    {
        Type type;
        var node = NextNode(out type, choice);
        if (type == typeof(TextDialogNode))
        {
            TextDialogNode textDialog = node as TextDialogNode;
            TextMesh.text = textDialog.dialogText;
            RegenerateChoices(new List<(string, int)> { ("Next", 0) });
        }
        else if (type == typeof(DecisionDialogNode))
        {
            DecisionDialogNode decisionDialog = node as DecisionDialogNode;
            TextMesh.text = decisionDialog.dialogText;
            RegenerateChoices(decisionDialog.GetChoices());
        }
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
