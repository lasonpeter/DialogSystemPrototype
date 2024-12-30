using UnityEngine;
using UnityEngine.Serialization;
using XNode;

namespace DialogSystem.Logic
{
    [NodeTint("#a58401")]
    [CreateNodeMenu("Logic/XOR")]
    public class XorNode : Node,IRecalculable
    {
        [Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.Strict)] public bool input1=false;
        [Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.Strict)] public bool input2=false;

        [Output(ShowBackingValue.Always, ConnectionType.Multiple, TypeConstraint.Strict)] public bool output;
        
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
                    input1 = inputPort1.GetInputValue<bool>();
                }
                if (inputPort2.IsConnected)
                {
                    input2 = inputPort2.GetInputValue<bool>();
                }
                //output = !GetInputValue<bool>("Input");
                // output = !input;
                return input1 != input2;
            }
            else
            {
                return null;
            }
        }

        public void OnValidate()
        {
            /*var inputPort1= GetInputPort("input1");
            var inputPort2= GetInputPort("input2");
            if (inputPort1.IsConnected)
            {
                input1 = inputPort1.GetInputValue<bool>();
            }
            if (inputPort2.IsConnected)
            {
                input2 = inputPort2.GetInputValue<bool>();
            }
            /*
            foreach (var outputPort in Outputs)
            {
                outputPort.node
            }#1#
            //Debug.Log(input);
            //output = !GetInputValue<bool>("Input");
            output = input1 != input2;*/
            DialogGraph dialogGraph = graph as DialogGraph;
            if(dialogGraph != null)
            {
                dialogGraph.RecalculateNodes();
            }
        }

        public void Recalculate()
        {
            var inputPort1= GetInputPort("input1");
            var inputPort2= GetInputPort("input2");
            if (inputPort1.IsConnected)
            {
                input1 = inputPort1.GetInputValue<bool>();
            }
            if (inputPort2.IsConnected)
            {
                input2 = inputPort2.GetInputValue<bool>();
            }        
            output = input1 != input2;
        }
    }
}