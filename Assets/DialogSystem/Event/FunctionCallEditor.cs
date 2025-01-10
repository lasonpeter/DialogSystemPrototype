using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using DialogSystem.Event;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using XNode;
using XNodeEditor;

namespace DialogSystem.Input
{
    [CustomNodeEditor(typeof(FunctionCallNode))]
    public class FunctionCallEditor:NodeEditor
    {
        private int _offsetPerNode = 20;
        private int _generalOffset = 120;
        private FunctionCallNode simpleNode;

        private void HandleItemClicked(object parameter)
        { 
            simpleNode = target as FunctionCallNode;
            {
                while (simpleNode.DynamicOutputs.ToArray().Length>0)
                {
                    simpleNode.RemoveDynamicPort(simpleNode.DynamicOutputs.ToArray()[0]);
                }
                while (simpleNode.DynamicInputs.ToArray().Length>0)
                {
                    simpleNode.RemoveDynamicPort(simpleNode.DynamicInputs.ToArray()[0]);
                }
                simpleNode.selectedMethod = (ClassMethod)parameter;
                var z = simpleNode.gameObject.GetComponent(simpleNode.selectedMethod.classType);
                MethodInfo methodInfo= z.GetType().GetMethod(simpleNode.selectedMethod.methodName,simpleNode.selectedMethod.MethodTypes.ToArray());
                Debug.LogWarning(methodInfo.Name);
                if(methodInfo.ReturnType != typeof(void))
                {
                    simpleNode.AddDynamicOutput(methodInfo.ReturnType, Node.ConnectionType.Multiple,
                        Node.TypeConstraint.None, methodInfo.ReturnType.Name.Split(".").Last());
                }

                foreach (var methodParameter in methodInfo.GetParameters())
                {
                    simpleNode.AddDynamicInput(methodParameter.ParameterType, Node.ConnectionType.Override,
                        Node.TypeConstraint.None, methodParameter.Name);
                }
            }
        }
        public override void OnBodyGUI() {
            base.OnBodyGUI(); 
            if (!simpleNode) simpleNode = target as FunctionCallNode;
            int x = 0;
            x =+ simpleNode.DynamicOutputs.ToArray().Length + simpleNode.DynamicInputs.ToArray().Length ;
            EditorGUILayout.Space(20);
            if(simpleNode && simpleNode.gameObject){
                if(simpleNode.selectedMethod.classType=="")
                {
                    if (EditorGUI.DropdownButton(new Rect(new Vector2(20, x*_offsetPerNode+_generalOffset), new Vector2(170, 20)), new GUIContent("Chose method"), FocusType.Keyboard))
                    {
                        GenericMenu menu = new GenericMenu();
                        foreach (var classMethods in simpleNode.ClassMethodsList)
                        {
                            //menu.AddSeparator(classMethods.Obj.GetType().Name);

                            foreach (var method in classMethods.MethodInfo)
                            {
                                if (method.GetParameters().Length > 0)
                                {
                                    string compoundName = method.Name + "( ";
                                    foreach (var parameterInfo in method.GetParameters())
                                    {
                                        compoundName += " "+ parameterInfo.ParameterType.Name + " " + parameterInfo.Name;
                                    }
                                    compoundName += " )";
                                    List<Type> types = new();
                                    foreach (var parameterInfo in method.GetParameters())
                                    {
                                        types.Add(parameterInfo.ParameterType);
                                    }
                                    menu.AddItem(new GUIContent(classMethods.Obj.GetType().Name+"/"+compoundName), false, HandleItemClicked, new ClassMethod(){methodName = method.Name,classType = classMethods.Obj.GetType().Name,MethodTypes = types});    
                                }
                                else
                                {
                                    menu.AddItem(new GUIContent(classMethods.Obj.GetType().Name+"/"+method.Name), false, HandleItemClicked, new ClassMethod(){methodName = method.Name,classType = classMethods.Obj.GetType().Name,MethodTypes = new List<Type>()}); 
                                }
                            }
                            
                        }

                        menu.DropDown(new Rect(20, x*_offsetPerNode+_generalOffset, 170, 20));
                    }
                }
                else
                {
                    string name = simpleNode.selectedMethod.classType+"."+simpleNode.selectedMethod.methodName;
                    if (EditorGUI.DropdownButton(new Rect(new Vector2(20, x*_offsetPerNode+_generalOffset), new Vector2(170, 20)), new GUIContent(name), FocusType.Keyboard))
                    {
                        GenericMenu menu = new GenericMenu();
                        foreach (var classMethods in simpleNode.ClassMethodsList)
                        {
                            //menu.AddSeparator(classMethods.Obj.GetType().Name);
                            foreach (var method in classMethods.MethodInfo)
                            {
                                if (method.GetParameters().Length > 0)
                                {
                                    string compoundName = method.Name + "( ";
                                    foreach (var parameterInfo in method.GetParameters())
                                    {
                                        compoundName += " "+ parameterInfo.ParameterType.Name + " " + parameterInfo.Name;
                                    }
                                    compoundName += " )";
                                    List<Type> types = new();
                                    foreach (var parameterInfo in method.GetParameters())
                                    {
                                        types.Add(parameterInfo.ParameterType);
                                    }
                                    menu.AddItem(new GUIContent(classMethods.Obj.GetType().Name+"/"+compoundName), false, HandleItemClicked, new ClassMethod(){methodName = method.Name,classType = classMethods.Obj.GetType().Name,MethodTypes = types});    
                                }
                                else
                                {
                                    menu.AddItem(new GUIContent(classMethods.Obj.GetType().Name+"/"+method.Name), false, HandleItemClicked, new ClassMethod(){methodName = method.Name,classType = classMethods.Obj.GetType().Name,MethodTypes = new List<Type>()});    
                                }
                            }
                        }
                        menu.DropDown(new Rect(20, x*_offsetPerNode+_generalOffset, 170, 20));
                    }    
                }
                
            }
            
             // Creates a 15-pixel gap
            // Update serialized object's representation
            serializedObject.Update();

            // Apply property modifications
            serializedObject.ApplyModifiedProperties();
        }

    }
}