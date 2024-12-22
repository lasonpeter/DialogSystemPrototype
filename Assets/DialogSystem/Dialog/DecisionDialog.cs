using UnityEngine;
using XNode;
[CreateNodeMenu("Dialog/DecisionDialog")]
[NodeTint("#145744")]
public class DecisionDialog : Node
{
	[TextArea] public string DialogText;
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
}