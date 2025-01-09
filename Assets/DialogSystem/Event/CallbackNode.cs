using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
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

		public string nodeId;
		
		[Serialize]
		public ClassMethod selectedMethod;
		
		// Use this for initialization
		protected override void Init() {
			base.Init();
			if(nodeId is null)
				nodeId = Guid.NewGuid().ToString();
		}
	

		private void OnValidate()
		{
			ClassMethodsList = new List<ClassMethods>();
			//this.graph.TriggerOnValidate();
			// this would cause an infinite loop
			if(gameObject != null)
			{
				GraphReader graphReader = FindObjectOfType<GraphReader>();
				if(graphReader.GameObjectsReferences.ContainsKey(nodeId)) {
					graphReader.GameObjectsReferences.Remove(nodeId);
				}
				graphReader.GameObjectsReferences.Add(nodeId,gameObject);
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
			//MethodInfo methodInfo = new DynamicMethod(selectedMethod.methodName, typeof(void), new Type[] {typeof(int)});
			GraphReader graphReader= FindObjectOfType<GraphReader>();
			graphReader.GameObjectsReferences.TryGetValue(nodeId, out var gameObject);
			var z = gameObject.GetComponent(selectedMethod.classType);
			var methodInfo= z.GetType().GetMethod(selectedMethod.methodName);
			methodInfo.Invoke(gameObject.GetComponent(selectedMethod.classType), new object[] { 32 });
			//var we= Outputs.ToArray()[chosenIndex].GetConnection(0).node as ITraversable;
			return Outputs.ToArray()[chosenIndex].GetConnection(0).node;
			//return Outputs.ToArray()[chosenIndex].GetConnection(0).node;
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
		public string classType;
		public string methodName;
	}
}