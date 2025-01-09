using System;
using System.Reflection;
using DialogSystem.Event;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using XNodeEditor;

namespace DialogSystem.Input
{
    [CustomNodeEditor(typeof(CallbackNode))]
    public class CallbackEditor:NodeEditor
    {
        private CallbackNode simpleNode;
        void handleItemClicked(object parameter)
        {
            if (!simpleNode) simpleNode = target as CallbackNode;
            {
                //Debug.Log("Menu selected: " + parameter);
                /*var we = (ClassMethod)parameter;
                we.objectId = Guid.NewGuid().ToString();*/
                simpleNode.selectedMethod = (ClassMethod)parameter;
            }
        }
        public override void OnBodyGUI() {
            base.OnBodyGUI(); 
            if (!simpleNode) simpleNode = target as CallbackNode;
            
            if(simpleNode && simpleNode.gameObject){
                GUILayout.Space(40);
                if(simpleNode.selectedMethod.classType=="")
                {
                    if (EditorGUI.DropdownButton(new Rect(new Vector2(20, 150), new Vector2(170, 20)), new GUIContent("Chose method"), FocusType.Keyboard))
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
                                    
                                    menu.AddItem(new GUIContent(classMethods.Obj.GetType().Name+"/"+compoundName), false, handleItemClicked, new ClassMethod(){methodName = method.Name,classType = classMethods.Obj.GetType().Name});;    
                                }
                                else
                                {
                                    menu.AddItem(new GUIContent(classMethods.Obj.GetType().Name+"/"+method.Name), false, handleItemClicked, new ClassMethod(){methodName = method.Name,classType = classMethods.Obj.GetType().Name});;    
                                }
                            }
                            
                        }

                        menu.DropDown(new Rect(20, 150, 170, 0));
                    }
                }
                else
                {
                    string name = simpleNode.selectedMethod.classType+"."+simpleNode.selectedMethod.methodName;
                    if (EditorGUI.DropdownButton(new Rect(new Vector2(20, 150), new Vector2(170, 20)), new GUIContent(name), FocusType.Keyboard))
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
                                    menu.AddItem(new GUIContent(classMethods.Obj.GetType().Name+"/"+compoundName), false, handleItemClicked, new ClassMethod(){methodName =method.Name,classType = classMethods.Obj.GetType().Name});;    
                                }
                                else
                                {
                                    menu.AddItem(new GUIContent(classMethods.Obj.GetType().Name+"/"+method.Name), false, handleItemClicked, new ClassMethod(){methodName = method.Name,classType = classMethods.Obj.GetType().Name});;    
                                }
                            }
                        }

                        menu.DropDown(new Rect(20, 150, 170, 0));
                    }    
                }
            }

            // Update serialized object's representation
            serializedObject.Update();

            // Apply property modifications
            serializedObject.ApplyModifiedProperties();
        }

    }
}