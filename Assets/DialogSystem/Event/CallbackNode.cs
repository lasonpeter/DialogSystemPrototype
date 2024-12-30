using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using XNode;

namespace DialogSystem.Event
{
	[CreateNodeMenu("Callback")]
	[NodeTint("#930291")]
	public class CallbackNode : Node, ITraversable {

		[Input(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None)] public string entry;
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None)] public string exit;
		
		[NonSerialized] public List<ClassMethods> ClassMethodsList;
		[SerializeField]
		public SerializedProperty serializedProperty;
		[SerializeField]
		public GameObject gameObject;
		
		[Serialize]
		public ClassMethod selectedMethod;
		
		// Use this for initialization
		protected override void Init() {
			base.Init();
		}
	
	
	

		private void OnValidate()
		{
			
			ClassMethodsList = new List<ClassMethods>();
			//this.graph.TriggerOnValidate();
			// this would cause an infinite loop
			if(gameObject != null)
			{
				var w = gameObject.GetComponents<Component>();
				foreach (var component in w)
				{
					Debug.Log(component.GetType());
					var type = component.GetType();
					var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);
				
					Debug.Log(methods.Length);
					foreach (var method in methods)
					{
						try
						{
							Debug.Log(method.Name+method);
							//MethodInfos.Add(new ClassMethods(){methodInfo  });
						}
						catch (Exception e)
						{
							Debug.LogWarning("It shit itself");
						}
					}
					ClassMethodsList.Add(new ClassMethods(){MethodInfo = methods,Obj = component});
				}
			}
		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port) {
			return null; // Replace this
		}

		public Node NextNode(int chosenIndex)
		{
			throw new NotImplementedException();
		}
	}
	public class ClassMethods
	{
		public MethodInfo[] MethodInfo;
		public object Obj;
	}
	
	[Serializable]
	public class ClassMethod
	{
		public string ClassType;
		public string MethodName;
	}
}