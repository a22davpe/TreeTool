using Codice.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyTool.Editor
{
    public class MyToolEditorWindow : EditorWindow
    {
        ToolAsset m_currentTool;

        public static void Open(ToolAsset target)
        {
            MyToolEditorWindow[] windows = Resources.FindObjectsOfTypeAll<MyToolEditorWindow>();

            foreach (MyToolEditorWindow window in windows)
            {
                if(window.currentTree == target){
                    window.Focus();
                    return;
                }
            }
        }

        [SerializeField]
        ToolAsset m_currentTree;

        [SerializeField]
        SerializedObject m_serializedObject;

        [SerializeField]
        MyToolView m_currentView;

        public ToolAsset currentTree => m_currentTree;

    }
}
