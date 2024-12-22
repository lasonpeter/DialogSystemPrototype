using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DialogSystem.Logic;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;

public class GraphReader : MonoBehaviour
{
    public SceneGraph sceneGraph;
    public Node currentNode;

    public Root rootNode;
    // Start is called before the first frame update
    void Start()
    {
        rootNode = sceneGraph.graph.nodes.Find(x => x is Root) as Root;
        currentNode= rootNode;
        var outputs = currentNode.Outputs;
        if (outputs.Count() == 1)
        {
            currentNode = outputs.ToArray()[0].GetConnection(0).node;
            Debug.Log(currentNode.GetType());
            //write a case for each type of node to cast it to its right type
            switch (currentNode)
            {
                case Root root:
                    Debug.Log("Root");
                    break;
                case TextDialog textDialog:
                    Debug.Log(textDialog.dialogText);
                    break;
                case DecisionDialog decisionDialog:
                    Debug.Log(decisionDialog.DialogText);
                    for (int i = 0; i < decisionDialog.Answers.Length; i++)
                    {
                        Debug.Log(decisionDialog.Answers[i] + "|Answers " + i);
                    } 
                    break;
            }
        }

        NextNode();
        NextNode(1);
        NextNode();
    }
    
    /// <summary>
    /// Returns the reference to the next node
    /// </summary>
    /// <param name="index">Which option was selected, default is the first one (0)</param>
    Node NextNode(int index=0)
    {
        var outputs = currentNode.Outputs;
            currentNode = outputs.ToArray()[index].GetConnection(0).node;
            Debug.Log(currentNode.GetType());
            if (IsCalculable(currentNode))
            {
                var we= currentNode.GetInputValue<bool>("Input");
                Debug.Log(we);
                //TODO calculate the node and return next node requiring user input
            }
            else
            {
                var we= currentNode.GetInputValue<string>("Input");
                Debug.Log(we);
            }
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
