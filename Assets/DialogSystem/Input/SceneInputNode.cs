using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DialogSystem.Input;
using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

[CreateNodeMenu("Input/SceneInput")]
[NodeTint("#5f00e5")]
public class SceneInputNode : Node {
	[NonSerialized]
	public List<string> fieldNames;

	public List<ClassFields> ClassFieldsList;
	[SerializeField]
	public ClassField SelectedField;
	[SerializeField]
	public SerializedProperty serializedProperty;
	[SerializeField]
	public GameObject gameObject;
	//public string nodeId;
	
	[NonSerialized]
	public string selectedField="";
	
	// Use this for initialization
	protected override void Init() {
		base.Init();
		/*if(nodeId is null)
			nodeId = Guid.NewGuid().ToString();*/
	}
	
	private void OnValidate()
	{
		ClassFieldsList = new List<ClassFields>();
		fieldNames = new List<string>();
		//this.graph.TriggerOnValidate();
		// this would cause an infinite loop
		if(gameObject != null){
			/*GraphReader graphReader = FindObjectOfType<GraphReader>();
			if(graphReader.GameObjectsReferences.ContainsKey(nodeId)) {
				graphReader.GameObjectsReferences.Remove(nodeId);
			}
			graphReader.GameObjectsReferences.Add(nodeId,gameObject);*/
			var w = gameObject.GetComponents<Component>();
			foreach (var component in w)
			{
				Debug.Log(component.GetType());
				var type = component.GetType();
				var methods = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
				List<FieldInfo> fieldInfos = new();
				Debug.Log(methods.Length);
				
				foreach (var method in methods)
				{
					try
					{
						Debug.Log(method.Name);
						fieldInfos.Add(method);
						fieldNames.Add(method.Name);
					}
					catch (Exception e)
					{
						Debug.LogWarning("It shit itself");
					}
				}
				ClassFieldsList.Add(new ClassFields(){FieldInfos = methods,Obj = component});
				var z = type.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
				
				Debug.Log(methods.Length);
				foreach (var method in z)
				{
					try
					{
						Debug.Log(method.Name);
						fieldNames.Add(method.Name);
					}
					catch (Exception e)
					{
						Debug.LogWarning("It shit itself");
					}
				}
			}
		}
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		var z = gameObject.GetComponent(SelectedField.classType);
		FieldInfo fieldInfo = z.GetType().GetField(SelectedField.variableName);
		return fieldInfo.GetValue(z);
	}
	
	public override void OnCreateConnection(NodePort from, NodePort to)
	{
		base.OnCreateConnection(from, to);
		OnValidate();//shutup
	}
}

[Serializable]
public class ClassField
{
	public string classType;
	public string variableName;
}

public class ClassFields
{
	public FieldInfo[] FieldInfos;
	public object Obj;
}