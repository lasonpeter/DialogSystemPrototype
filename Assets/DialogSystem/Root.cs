using System.Linq;
using DialogSystem;
using UnityEngine;
using XNode;

[DisallowMultipleNodes]
[NodeTint("#b50903")]
public class Root : Node, ITraversable{
	[Output(ShowBackingValue.Never,ConnectionType.Override)]
	public string Output;

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
		return Outputs.ToArray()[chosenIndex].GetConnection(0).node;
	}
}