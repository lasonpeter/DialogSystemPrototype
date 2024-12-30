using System;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;

namespace DialogSystem.Logic
{
    [NodeTint("#a58401")]
    [CreateNodeMenu("Logic/IF")]
    public class IfNode : Node, ITraversable
    {

        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)] public string entry;
        [SerializeField] private Comparator comparator;       
        [SerializeField] public FieldType fieldType;
        [Input(ShowBackingValue.Unconnected, ConnectionType.Multiple, TypeConstraint.None)] public bool input1;
        [Input(ShowBackingValue.Unconnected, ConnectionType.Multiple, TypeConstraint.None)] public bool input2;
        
        [Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None)] public string True;
        [Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None)] public string False;
        public Node NextNode(int chosenIndex)
        {
            if (fieldType == FieldType.Float){
                switch (comparator)
                {
                    case Comparator.Equals:
                        return (bool)GetPort("input1").GetInputValue() == (bool)GetPort("input2").GetInputValue() ? GetOutputPort("True").Connection.node : GetOutputPort("False").Connection.node;
                    case Comparator.NotEquals:
                        return (bool)GetPort("input1").GetInputValue() != (bool)GetPort("input2").GetInputValue() ? GetOutputPort("True").Connection.node : GetOutputPort("False").Connection.node;
                }
            }
            else if(fieldType == FieldType.Float)
            {
                switch (comparator)
                {
                    case Comparator.Equals:
                        return Mathf.Approximately((float)GetPort("input1").GetInputValue(), (float)GetPort("input2").GetInputValue()) ? GetOutputPort("True").Connection.node : GetOutputPort("False").Connection.node;
                    case Comparator.NotEquals:
                        return !Mathf.Approximately((float)GetPort("input1").GetInputValue(), (float)GetPort("input2").GetInputValue()) ? GetOutputPort("True").Connection.node : GetOutputPort("False").Connection.node;
                }
            }
            return null;
        }

    }
    public enum FieldType
    {
        Bool,
        Float
    }
    public enum Comparator
    {
        Equals,
        NotEquals,
        GreaterThan,
        LessThan,
        GreaterThanOrEquals,
        LessThanOrEquals
    }
}