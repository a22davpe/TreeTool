using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using TMPro.EditorUtilities;

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

            TMP_EditorPanel.CreateEditor(asset.Text);
        }
    }
}
