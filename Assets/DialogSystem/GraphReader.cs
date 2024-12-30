using System;
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
    
    /// <summary>
    /// Loads up the dialog by reading the root node
    /// </summary>
    void Start()
    {
        rootNode = sceneGraph.graph.nodes.Find(x => x is Root) as Root;
        currentNode= rootNode;
        var outputs = currentNode.Outputs;
        if (outputs.Count() == 1)
        {
            currentNode = outputs.ToArray()[0].GetConnection(0).node;
            Debug.Log(currentNode.GetType());
        }
    }

    /// <summary>
    /// Returns the reference to the next node
    /// </summary>
    /// <param name="nodeType">Type of the node returned</param>
    /// <param name="index">Which option was selected, default is the first one (0)</param>
    Node NextNode(out Type nodeType, int index = 0)
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
