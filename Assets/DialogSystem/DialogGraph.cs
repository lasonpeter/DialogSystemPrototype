using System;
using System.Collections;
using System.Collections.Generic;
using DialogSystem;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;


[CreateAssetMenu]
public class DialogGraph : NodeGraph
{

	[SerializeField] public string dialogName;
	public void RecalculateNodes()
	{
		foreach (var node in nodes)
		{
			IRecalculable recalculable= node as IRecalculable;
			recalculable?.Recalculate();
		}
	}
}