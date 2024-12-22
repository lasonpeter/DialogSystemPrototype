using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

[CreateAssetMenu]
public class DialogGraph : NodeGraph {
	private static float lastUpdateTime;
	private static float updateInterval = 1f;

	public void EditorUpdate()
	{
		if(Time.realtimeSinceStartup - lastUpdateTime > updateInterval)
		{
			foreach (var node in nodes)
			{
				node.TriggerOnValidate();
			}
			lastUpdateTime = Time.realtimeSinceStartup;
		}
	}
	private void OnEnable()
	{
		EditorApplication.update += EditorUpdate;
		lastUpdateTime = Time.realtimeSinceStartup;
	}
}