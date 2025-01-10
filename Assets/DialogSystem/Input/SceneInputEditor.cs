using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using DialogSystem.Event;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using XNode;
using XNodeEditor;
using GUIContent = UnityEngine.GUIContent;

namespace DialogSystem.Input
{
    [NodeEditor.CustomNodeEditor(typeof(SceneInputNode))]
    public class SceneInputEditor:NodeEditor
    {
        
        private SceneInputNode simpleNode;
        void handleItemClicked(object parameter)
        {
            if (!simpleNode) simpleNode = target as SceneInputNode;
            {
                //simpleNode.DynamicOutputs = Enumerable.Empty<NodePort>();
                //Debug.Log("Menu selected: " + parameter);
                while (simpleNode.DynamicOutputs.ToArray().Length>0)
                {
                    simpleNode.RemoveDynamicPort(simpleNode.DynamicOutputs.ToArray()[0]);
                }

                simpleNode.SelectedField = (ClassField)parameter;
                var z = simpleNode.gameObject.GetComponent(simpleNode.SelectedField.classType);
                FieldInfo fieldInfo= z.GetType().GetField(simpleNode.SelectedField.variableName);
                Debug.LogWarning(fieldInfo.FieldType.ToString());
                simpleNode.AddDynamicOutput(fieldInfo.FieldType, Node.ConnectionType.Multiple,
                    Node.TypeConstraint.None, fieldInfo.Name);
            }
        }
        public override void OnBodyGUI() {
            base.OnBodyGUI(); 
            if (!simpleNode) simpleNode = target as SceneInputNode;
            
            /*if(simpleNode && simpleNode.gameObject){
                GUILayout.Space(40);
                if (!EditorGUI.DropdownButton(new Rect(new Vector2(20, 70), new Vector2(170, 20)),
                        new GUIContent(simpleNode.selectedField), FocusType.Keyboard))
                {
                    return;
                }
                else
                {
                    GenericMenu menu = new GenericMenu();
                    foreach (var VARIABLE in simpleNode.f.fieldNames)
                    {
                        menu.AddItem(new GUIContent(VARIABLE), false, handleItemClicked, VARIABLE);
                    }

                    menu.DropDown(new Rect(0, 0, 170, 0));
                }
            }*/
            if(simpleNode && simpleNode.gameObject){
                GUILayout.Space(10);
                if(simpleNode.SelectedField.classType=="")
                {
                    if (EditorGUI.DropdownButton(new Rect(new Vector2(20, 90), new Vector2(170, 20)), new GUIContent("Chose method"), FocusType.Keyboard))
                    {
                        GenericMenu menu = new GenericMenu();
                        foreach (var classMethods in simpleNode.ClassFieldsList)
                        {
                            //menu.AddSeparator(classMethods.Obj.GetType().Name);

                            foreach (var field in classMethods.FieldInfos)
                            {
                                /*if (fieldInfo..Length > 0)
                                {*/
                                    /*string compoundName = method.Name + "( ";
                                    foreach (var parameterInfo in method.GetParameters())
                                    {
                                        compoundName += " "+ parameterInfo.ParameterType.Name + " " + parameterInfo.Name;
                                    }
                                    compoundName += " )";
                                    */
                                    
                                    menu.AddItem(new GUIContent(classMethods.Obj.GetType().Name+"/"+field.Name), false, handleItemClicked, new ClassField(){variableName = field.Name,classType = classMethods.Obj.GetType().Name});;    
                                /*}
                                else
                                {
                                    menu.AddItem(new GUIContent(classMethods.Obj.GetType().Name+"/"+method.Name), false, handleItemClicked, new ClassMethod(){methodName = method.Name,classType = classMethods.Obj.GetType().Name});;    
                                }*/
                            }
                            
                        }

                        menu.DropDown(new Rect(20, 110, 350, 0));
                    }
                }
                else
                {
                    string name = simpleNode.SelectedField.classType+"."+simpleNode.SelectedField.variableName;
                    if (EditorGUI.DropdownButton(new Rect(new Vector2(20, 90), new Vector2(170, 20)), new GUIContent(name), FocusType.Keyboard))
                    {
                        GenericMenu menu = new GenericMenu();
                        foreach (var classMethods in simpleNode.ClassFieldsList)
                        {
                            //menu.AddSeparator(classMethods.Obj.GetType().Name);
                            foreach (var field in classMethods.FieldInfos)
                            {
                                /*if (field.GetParameters().Length > 0)
                                {*/
                                    //string compoundName = method.Name + "( ";
                                    //foreach (var parameterInfo in fi.GetParameters())
                                    //{
                                    //    compoundName += " "+ parameterInfo.ParameterType.Name + " " + parameterInfo.Name;
                                    //}
                                    //compoundName += " )";
                                    menu.AddItem(new GUIContent(classMethods.Obj.GetType().Name+"/"+field.Name), false, handleItemClicked, new ClassField(){variableName = field.Name,classType = classMethods.Obj.GetType().Name});;    
                                /*}
                                else
                                {
                                    menu.AddItem(new GUIContent(classMethods.Obj.GetType().Name+"/"+method.Name), false, handleItemClicked, new ClassMethod(){methodName = method.Name,classType = classMethods.Obj.GetType().Name});;    
                                }*/
                            }
                        }

                        menu.DropDown(new Rect(20, 110, 350, 0));
                    }    
                }
               
            }
            
            //methodInfo.Invoke(gameObject.GetComponent(selectedMethod.classType), new object[] { 32 });
            // Update serialized object's representation
            serializedObject.Update();

            // Apply property modifications
            serializedObject.ApplyModifiedProperties();
        }

    }
}