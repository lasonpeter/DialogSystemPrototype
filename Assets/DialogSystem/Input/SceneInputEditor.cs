using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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
                //Debug.Log("Menu selected: " + parameter);
                simpleNode.selectedField = (string)parameter;
            }
        }
        public override void OnBodyGUI() {
            base.OnBodyGUI(); 
            if (!simpleNode) simpleNode = target as SceneInputNode;
            
            if(simpleNode && simpleNode.gameObject){
                GUILayout.Space(40);
                if (!EditorGUI.DropdownButton(new Rect(new Vector2(20, 70), new Vector2(170, 20)),
                        new GUIContent(simpleNode.selectedField), FocusType.Keyboard))
                {
                    return;
                }
                else
                {
                    GenericMenu menu = new GenericMenu();
                    foreach (var VARIABLE in simpleNode.fieldNames)
                    {
                        menu.AddItem(new GUIContent(VARIABLE), false, handleItemClicked, VARIABLE);
                    }

                    menu.DropDown(new Rect(0, 0, 170, 0));
                }
            }

            // Update serialized object's representation
            serializedObject.Update();

            // Apply property modifications
            serializedObject.ApplyModifiedProperties();
        }

    }
}