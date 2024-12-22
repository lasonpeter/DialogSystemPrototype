using UnityEngine.Serialization;
using XNode;

namespace DialogSystem.Math
{
    [NodeTint("#004B76")]
    [CreateNodeMenu("Math/Multiply")]
    public class MultiplyNode : Node
    {
        [Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.Strict)] public float input1=0;
        [Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.Strict)] public float input2=0;

        [Output(ShowBackingValue.Always, ConnectionType.Multiple, TypeConstraint.Strict)] public float output;
        
        
        public override object GetValue(NodePort port) {
            if(port.fieldName== "output")
            {
                var inputPort1= GetInputPort("input1");
                var inputPort2= GetInputPort("input2");
                if (inputPort1.IsConnected)
                {
                    input1 = inputPort1.GetInputValue<float>();
                }
                if (inputPort2.IsConnected)
                {
                    input2 = inputPort2.GetInputValue<float>();
                }
                //output = !GetInputValue<float>("Input");
                // output = !input;
                return input1 * input2;
            }
            else
            {
                return null;
            }
        }

        public void OnValidate()
        {
            var inputPort1= GetInputPort("input1");
            var inputPort2= GetInputPort("input2");
            if (inputPort1.IsConnected)
            {
                input1 = inputPort1.GetInputValue<float>();
            }
            if (inputPort2.IsConnected)
            {
                input2 = inputPort2.GetInputValue<float>();
            }
            /*
            foreach (var outputPort in Outputs)
            {
                outputPort.node
            }*/
            //Debug.Log(input);
            //output = !GetInputValue<float>("Input");
            output = input1 * input2;
        }
    }
}