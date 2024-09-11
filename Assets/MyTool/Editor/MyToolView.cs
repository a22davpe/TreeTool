using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
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

        public Dictionary<Edge, ToolConnection> m_connectionDictionary;

        ToolWindowSearchProvider m_searchProvider;

        public MyToolView(SerializedObject serializedObject, MyToolEditorWindow window)
        {
            m_serializedObject = serializedObject;
            m_tool = (ToolAsset)serializedObject.targetObject;
            m_window = window;

            m_nodeDictionary = new Dictionary<string, ToolEditorNode>();
            m_connectionDictionary = new Dictionary<Edge, ToolConnection>();
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
            DrawConnections();

            graphViewChanged += OnGraphViewChangedEvent;
        }

        private GraphViewChange OnGraphViewChangedEvent(GraphViewChange graphViewChange)
        {
            if(graphViewChange.movedElements != null)
            {
                Undo.RecordObject(m_serializedObject.targetObject, "Moved things ín graph");
                foreach (ToolEditorNode editorNode in graphViewChange.movedElements.OfType<ToolEditorNode>())
                {
                    editorNode.SavePosition();
                }
            }


            if (graphViewChange.elementsToRemove != null)
            {
                Undo.RecordObject(m_serializedObject.targetObject, "Remove things from graph");

                List<ToolEditorNode> nodes = graphViewChange.elementsToRemove.OfType<ToolEditorNode>().ToList();

                if (nodes.Count > 0)
                {
                    for (int i = nodes.Count - 1; i >= 0; i--)
                    {
                        RemoveNode(nodes[i]);
                    }
                }

                foreach (Edge edge in graphViewChange.elementsToRemove.OfType<Edge>())
                {
                    RemoveConnection(edge);
                }
            }

            if(graphViewChange.edgesToCreate != null)
            {
                Undo.RecordObject(m_serializedObject.targetObject, "Added connections");
                foreach (Edge edge in graphViewChange.edgesToCreate)
                {
                    CreateEdge(edge);
                }
            }

            return graphViewChange;
        }

        #region NodeFunctions

        private void DrawNodes()
        {
            foreach (ToolNode node in m_tool.Nodes)
            {
                AddNodeToTree(node);

            }
            Bind();
        }

        private ToolEditorNode GetNode(string nodeId)
        {
            ToolEditorNode node = null;

            m_nodeDictionary.TryGetValue(nodeId, out node);

            return node;
        }

        private void RemoveNode(ToolEditorNode editorNode)
        {
            m_tool.Nodes.Remove(editorNode.Node);
            m_nodeDictionary.Remove(editorNode.Node.id);
            m_toolNodes.Remove(editorNode);
            m_serializedObject.Update();
        }

        public void Add(ToolNode node)
        {
            Undo.RecordObject(m_serializedObject.targetObject, "Added Node");

            m_tool.Nodes.Add(node);

            m_serializedObject.Update();

            AddNodeToTree(node);
            Bind();
        }

        private void AddNodeToTree(ToolNode node)
        {
            node.typeName = node.GetType().AssemblyQualifiedName;

            ToolEditorNode editorNode = new ToolEditorNode(node, m_serializedObject);
            editorNode.SetPosition(node.position);

            m_toolNodes.Add(editorNode);
            m_nodeDictionary.Add(node.id, editorNode);
            AddElement(editorNode);
        }

        #endregion

        #region Connections and Ports

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> allPorts = new List<Port>();
            List<Port> ports = new List<Port>();

            foreach (var node in m_toolNodes)
            {
                allPorts.AddRange(node.Ports);
            }

            foreach (Port p in allPorts)
            {
                if (p == startPort) continue;
                if (p.node == startPort.node) continue;
                if (p.direction == startPort.direction) continue;

                if (p.portType == startPort.portType)
                    ports.Add(p);

            }

            return ports;
        }

        private void RemoveConnection(Edge edge)
        {
            if (m_connectionDictionary.TryGetValue(edge, out ToolConnection connection))
            {
                m_tool.Connections.Remove(connection);
                m_connectionDictionary.Remove(edge);
            }
        }

        private void CreateEdge(Edge edge)
        {
            ToolEditorNode inputNode = (ToolEditorNode)edge.input.node;
            int inputIndex = inputNode.Ports.IndexOf(edge.input);

            ToolEditorNode outputNode = (ToolEditorNode)edge.output.node;
            int outputIndex = outputNode.Ports.IndexOf(edge.output);

            ToolConnection connection = new ToolConnection(inputNode.Node.id, inputIndex, outputNode.Node.id, outputIndex);

            m_tool.Connections.Add(connection);
            m_connectionDictionary.Add(edge, connection);
        }

        private void DrawConnections()
        {
            if (m_tool.Connections == null)
                return;

            foreach (ToolConnection connetion in m_tool.Connections)
            {
                DrawConnection(connetion);
            }
        }

        private void DrawConnection(ToolConnection connetion)
        {
            ToolEditorNode inputNode = GetNode(connetion.inputPort.nodeId);
            ToolEditorNode outputNode = GetNode(connetion.outputPort.nodeId);


            if (inputNode == null)
            {
                Debug.LogError("inputNode is null in connection drawern!!");
                return;
            }
            if (outputNode == null)
            {
                Debug.LogError("outputNode is null in connection drawern!!");
                return;
            }

            Port inPort = inputNode.Ports[connetion.inputPort.portIndex];
            Port outPort = outputNode.Ports[connetion.outputPort.portIndex];
            Edge edge = inPort.ConnectTo(outPort);
            AddElement(edge);

            m_connectionDictionary.Add(edge, connetion);
        }

        #endregion

        private void ShowSearchWindow(NodeCreationContext context)
        {
            m_searchProvider.target = (VisualElement)focusController.focusedElement;
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition),m_searchProvider);
        }

        private void Bind()
        {
            m_serializedObject.Update();

            //inte samma bind, denna kommer ifrån visualelement
            this.Bind(m_serializedObject);

        }
    }
}
