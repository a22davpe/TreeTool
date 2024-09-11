using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using TMPro.EditorUtilities;
using TMPro;

namespace MyTool.Editor
{
    [CustomEditor(typeof(ToolAsset))]
    public class ToolAssetEditor : UnityEditor.Editor
    {
        ToolAsset asset;


        //If you double click on a tool asset it opens
        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId, int index)
        {
            //Gets asset clicked on
            Object asset = EditorUtility.InstanceIDToObject(instanceId);
            
            //If it's a toolAsset open it
            if(asset.GetType() == typeof(ToolAsset))
            {
                MyToolEditorWindow.Open((ToolAsset)asset);
                return true;
            }
            return false;
        }

        private void OnEnable()
        {
            asset = target as ToolAsset;
        }


        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Editor"))
            {
                MyToolEditorWindow.Open((ToolAsset)target);
            }

            EditorGUI.BeginChangeCheck();

            asset.numberOfSpeaker = EditorGUILayout.IntField("Speakers",asset.numberOfSpeaker, GUILayout.Height(20));

            if (asset.numberOfSpeaker < 0)
                asset.numberOfSpeaker = 0;

            if (asset.speechBubbles.Count != asset.numberOfSpeaker)
            { 
                for (int i = 0; i < asset.numberOfSpeaker; i++)
                {
                    if (i == asset.speechBubbles.Count)
                        asset.speechBubbles.Add(null);

                    asset.speechBubbles[i] = (TMP_Text)EditorGUILayout.ObjectField($"Speaker {i + 1}", asset.speechBubbles[i], typeof(TMP_Text), true, GUILayout.Height(20));
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.Update();
                EditorUtility.SetDirty(asset);
            }
        }
    }
}
