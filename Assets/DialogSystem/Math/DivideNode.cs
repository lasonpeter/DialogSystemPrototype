using UnityEngine;
using UnityEngine.Serialization;
using XNode;

namespace DialogSystem.Math
{
    [NodeTint("#004B76")]
    [CreateNodeMenu("Math/Divide")]
    public class DivideNode : Node, IRecalculable
    {
        [Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.Strict)] public float input1=1;
        [Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.Strict)] public float input2=1;

        [Output(ShowBackingValue.Always, ConnectionType.Multiple, TypeConstraint.Strict)] public float output;
        protected override void Init() {
            base.Init();
        }
        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);
            OnValidate();//shutup
        }
        
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
                if (input2 == 0)
                {
                    return 0;
                }
                return input1 / input2;
            }
            else
            {
                return null;
            }
        }

        public void OnValidate()
        {
            DialogGraph dialogGraph = graph as DialogGraph;
            if (dialogGraph != null) {
                dialogGraph.RecalculateNodes();
            }
        }

        public void Recalculate()
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
            if (input2 == 0)
            {
                output = 0;
                return;
            }
            output = input1 / input2;
        }
    }
}