using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;

namespace MyTool.Editor
{
    [CustomEditor(typeof(ToolAsset))]
    public class ToolAssetEditor : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Editor"))
            {
                MyToolEditorWindow.Open((ToolAsset)target);
            }
        }
    }
}
