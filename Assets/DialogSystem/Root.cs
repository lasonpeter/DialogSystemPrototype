using XNode;

[DisallowMultipleNodes]
[NodeTint("#b50903")]
public class Root : Node {
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
}