using XNode;
using XNodeEditor;

namespace DialogSystem.Logic
{
    [NodeEditor.CustomNodeEditor(typeof(IfNode))]
    public class IfNodeEditor: NodeEditor
    {
        public override void OnBodyGUI()
        {
            var ifNode = target as IfNode;
            switch (ifNode.fieldType)
            {
                case FieldType.Bool:
                    if(ifNode.GetPort("input1").ValueType != typeof(bool))
                    {
                        ifNode.GetPort("input1").ValueType = typeof(bool);
                        ifNode.GetPort("input2").ValueType = typeof(bool);
                    }
                    break;
                case FieldType.Float:
                    if(ifNode.GetPort("input1").ValueType != typeof(float))
                    {
                        ifNode.GetPort("input1").ValueType = typeof(float);
                        ifNode.GetPort("input2").ValueType = typeof(float);
                    }
                    break;
            }
            base.OnBodyGUI();
        }
    }
}