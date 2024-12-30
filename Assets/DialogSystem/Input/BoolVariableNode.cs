using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(140)]
[CreateNodeMenu("Input/Bool")]
[NodeTint("#6d6d6d")]
public class BoolVariableNode : Node {
	[Output(ShowBackingValue.Always, ConnectionType.Multiple, TypeConstraint.Strict)] 
	public Boolean output;
	
	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	public override void OnCreateConnection(NodePort from, NodePort to)
	{
		base.OnCreateConnection(from, to);
		OnValidate();//shutup
	}
	public override void OnRemoveConnection(NodePort port)
	{
		base.OnRemoveConnection(port);
		OnValidate();//shutup
	}
	public override object GetValue(NodePort port) {
		if(port.fieldName== "output")
		{
			return output;
		}
		else
		{
			return null;
		}
	}
	// Return the correct value of an output port when requested
		public void OnValidate()
		{
			DialogGraph dialogGraph = graph as DialogGraph;
			if(dialogGraph != null)
			{
				dialogGraph.RecalculateNodes();
			}
		}
}