using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyTool.Editor
{
    public class MyToolView : GraphView
    {
        private ToolAsset m_tool;
        private SerializedObject m_serializedObject;
        private MyToolEditorWindow m_window;

        public MyToolEditorWindow window => m_window;

        public List<ToolEditorNode> m_toolNodes;
        public Dictionary<string, ToolEditorNode> m_nodeDictionary;

        ToolWindowSearchProvider m_searchProvider;

        public MyToolView(SerializedObject serializedObject, MyToolEditorWindow window)
        {
            m_serializedObject = serializedObject;
            m_tool = (ToolAsset)serializedObject.targetObject;
            m_window = window;

            m_nodeDictionary = new Dictionary<string, ToolEditorNode>();
            m_toolNodes = new List<ToolEditorNode>();

            m_searchProvider = ScriptableObject.CreateInstance<ToolWindowSearchProvider>();
            m_searchProvider.tool = this;
            this.nodeCreationRequest = ShowSearchWindow;

            StyleSheet style = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/MyTool/Editor/USS/MyToolEditor.uss");
            styleSheets.Add(style);

            GridBackground backgrund = new GridBackground();
            backgrund.name = "Grid";
            Add(backgrund);
            backgrund.SendToBack();

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new ClickSelector());

            DrawNodes();
        }

        private void DrawNodes()
        {
            foreach (ToolNode node in m_tool.Nodes)
            {
                AddNodeToTree(node);
            }
        }

        private void ShowSearchWindow(NodeCreationContext context)
        {
            m_searchProvider.target = (VisualElement)focusController.focusedElement;
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition),m_searchProvider);
        }

        public void Add(ToolNode node)
        {
            Undo.RecordObject(m_serializedObject.targetObject, "Added Node");

            m_tool.Nodes.Add(node);

            m_serializedObject.Update();

            AddNodeToTree(node);
        }

        private void AddNodeToTree(ToolNode node)
        {
            node.typeName = node.GetType().AssemblyQualifiedName;

            ToolEditorNode editorNode = new ToolEditorNode(node);
            editorNode.SetPosition(node.position);

            m_toolNodes.Add(editorNode);
            m_nodeDictionary.Add(node.id, editorNode);
            AddElement(editorNode);
        }
    }
}
