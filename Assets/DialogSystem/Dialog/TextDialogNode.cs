using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DialogSystem;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;

[CreateNodeMenu("Dialog/TextDialog")]
[NodeTint("#145744")]
public class TextDialogNode : Node, ITraversable
{
	[TextArea]
	public string dialogText;
	public AudioClip audioClip;
	[Input(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None)]
	public string input;
	[Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)]
	public string output;
	// Use this for initialization
	protected override void Init()
	{
		base.Init();
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port)
	{
		return "output";
	}
	
	public Node NextNode(int chosenIndex)
	{
		Debug.Log(Outputs.ToArray().Length);
		foreach (var output in Outputs)
		{
			Debug.Log(output.ConnectionCount + output.fieldName);
		}
		return Outputs.ToArray()[chosenIndex].GetConnection(0).node;
	}
}