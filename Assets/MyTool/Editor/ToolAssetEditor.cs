using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEditor.Callbacks;

namespace MyTool.Editor
{
    [CustomEditor(typeof(ToolAsset))]
    public class ToolAssetEditor : UnityEditor.Editor
    {

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


        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Editor"))
            {
                MyToolEditorWindow.Open((ToolAsset)target);
            }
        }
    }
}
