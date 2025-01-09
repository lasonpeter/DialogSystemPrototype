using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DialogSystem;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;

public class FocusNode : Node, ITraversable
{
	[Input(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None)]
	public string _input;
	[Output(ShowBackingValue.Never,ConnectionType.Multiple,TypeConstraint.None)]
	public string _output;
	[SerializeField]
	public GameObject focusObject;
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
		return Outputs.ToArray()[chosenIndex].GetConnection(0).node;
	}
}