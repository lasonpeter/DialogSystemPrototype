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
	[SerializeField]
	public SerializedProperty serializedProperty;
	[SerializeField]
	public GameObject gameObject;
	
	
	[NonSerialized]
	public string selectedField="";
	
	// Use this for initialization
	protected override void Init() {
		base.Init();
	}
	
	
	

	private void OnValidate()
	{
		fieldNames = new List<string>();
		//this.graph.TriggerOnValidate();
		// this would cause an infinite loop
		if(gameObject != null){
			var w = gameObject.GetComponents<Component>();
			foreach (var component in w)
			{
				Debug.Log(component.GetType());
				var type = component.GetType();
				var methods = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
				
				Debug.Log(methods.Length);
				foreach (var method in methods)
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
		return null; // Replace this
	}
}