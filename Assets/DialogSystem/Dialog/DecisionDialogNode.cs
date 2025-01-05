using System.Collections.Generic;
using System.Linq;
using DialogSystem;
using UnityEngine;
using XNode;
[CreateNodeMenu("Dialog/DecisionDialog")]
[NodeTint("#145744")]
public class DecisionDialogNode : Node, ITraversable
{
	public string characterName;
	public Sprite sprite;
	[TextArea] public string dialogText;
	public AudioClip AudioClip;
	[Input(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None)]public string Input;
	[TextArea] [Output(ShowBackingValue.Never, ConnectionType.Override,TypeConstraint.None,dynamicPortList = true)] public string[] Answers;
	
	// Use this for initialization
	protected override void Init() {
		base.Init();
	}
	
	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}

	public Node NextNode(int chosenIndex)
	{
		Debug.Log(Outputs.ToArray().Length);
		foreach (var output in Outputs)
		{
			Debug.Log(output.ConnectionCount + output.fieldName);
		}
		Debug.Log($"Index:{chosenIndex}, amountOfNodes: {Outputs.ToArray().Length}");
		return Outputs.ToArray()[chosenIndex].GetConnection(0).node;
	}

	/// <summary>
	/// Returns all possible choices with their respective "ID"
	/// </summary>
	/// <returns>A list of Tuples, string being the text, int being the choice index</returns>
	public List<(string,int)> GetChoices()
	{
		List<(string,int)> choices = new();
		int x = Answers.Length-1;
		for (int i = Outputs.ToArray().Length-1; i >= 1; i--)
		{
			choices.Add((Answers[x],i));
			x--;
			//choices.Add((Outputs.ToArray()[i].fieldName,i));
		}
		return choices;
	}
}