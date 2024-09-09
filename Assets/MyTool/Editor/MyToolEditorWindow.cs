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
            foreach (var w in windows)
            {
                if (w.m_currentTool == target) { 
                    w.Focus();
                    return;
                }
            }

            MyToolEditorWindow window = CreateWindow<MyToolEditorWindow>(typeof(MyToolEditorWindow), typeof(SceneView));
            window.titleContent = new GUIContent($"{target.name}");
            window.Load(target);
        }

        [SerializeField]
        ToolAsset m_currentTree;

        [SerializeField]
        SerializedObject m_serializedObject;

        [SerializeField]
        MyToolView m_currentView;

        public ToolAsset currentTree => m_currentTree;

        public void Load(ToolAsset target)
        {
            m_currentTool = target;
            DrawTool();
        }

        private void DrawTool()
        {
            m_serializedObject = new SerializedObject(m_currentTool);
            m_currentView = new MyToolView(m_serializedObject, this);
            rootVisualElement.Add(m_currentView);
        }

    }
}
