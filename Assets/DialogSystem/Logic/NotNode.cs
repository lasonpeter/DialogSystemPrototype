using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;

namespace DialogSystem.Logic
{
    [CreateNodeMenu("Logic/NOT")]
    //Serialize the name as "NOT" instead of the default "Not"
    [NodeTint("#a58401")]
    public class Not : Node, IRecalculable
    {
        [Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.Strict)] public bool input;
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
                foreach (var inputNode in Inputs)
                {
                    if (inputNode.IsConnected)
                    {
                        //Debug.Log( GetInputValue<bool>("input",true));
                        input = GetInputValue<bool>("input");
                    }
                }
                //output = !GetInputValue<bool>("Input");
                // output = !input;
                return !input;
            }
            else
            {
                return null;
            }
        }

        public void OnValidate()
        {
            DialogGraph dialogGraph = graph as DialogGraph;
            if(dialogGraph != null)
            {
                dialogGraph.RecalculateNodes();
            }
            /*foreach (var inputNode in Inputs)
            {
                if (inputNode.IsConnected)
                {
                    input =GetInputValue<bool>("input");
                }
            }

            /*
            foreach (var outputPort in Outputs)
            {
                outputPort.node
            }#1#
            //Debug.Log(input);
            //output = !GetInputValue<bool>("Input");
            output = !input;*/
        }

        public void Recalculate()
        {
            foreach (var inputNode in Inputs)
            {
                if (inputNode.IsConnected)
                {
                    input =GetInputValue<bool>("input");
                }
            }

            /*
            foreach (var outputPort in Outputs)
            {
                outputPort.node
            }*/
            //Debug.Log(input);
            //output = !GetInputValue<bool>("Input");
            output = !input;
        }
    }
}