using Codice.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
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
        SerializedObject m_serializedObject;

        [SerializeField]
        MyToolView m_currentView;

        public ToolAsset currentTree => m_currentTool;


        private void OnEnable()
        {
            if(m_currentTool != null)
            {
                DrawTool();
            }
        }

        private void OnGUI()
        {
            if(m_currentTool != null)
            {
                if (EditorUtility.IsDirty(m_currentTool))
                    this.hasUnsavedChanges = true;
                else
                    this.hasUnsavedChanges = false;
            }
        }
        public void Load(ToolAsset target)
        {
            m_currentTool = target;
            DrawTool();
        }

        private void DrawTool()
        {
            m_serializedObject = new SerializedObject(m_currentTool);
            m_currentView = new MyToolView(m_serializedObject, this);
            m_currentView.graphViewChanged += OnChange;
            rootVisualElement.Add(m_currentView);
        }

        private GraphViewChange OnChange(GraphViewChange graphViewChange)
        {
            EditorUtility.SetDirty(m_currentTool);
            return graphViewChange;
        }
    }
}
