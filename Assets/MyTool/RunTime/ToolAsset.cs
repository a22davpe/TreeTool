using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace MyTool
{
    [CreateAssetMenu(menuName = "MyTool/New Tree")]
    public class ToolAsset : ScriptableObject
    {
        [SerializeReference]
        private List<ToolNode> m_nodes;
        [SerializeField]
        private List<ToolConnection> m_connections;

        [SerializeReference]
        public TMP_Text Text;

        private Dictionary<string, ToolNode> m_nodeDictionary;

        public List<ToolNode> Nodes => m_nodes;
        public List<ToolConnection> Connections => m_connections;

        public ToolAsset()
        {
            m_nodes = new List<ToolNode>();
            m_connections = new List<ToolConnection>();
        }

        public void CreateNodeDictionary()
        {
            m_nodeDictionary = new Dictionary<string, ToolNode>();
            foreach (ToolNode node in m_nodes)
            {
                m_nodeDictionary.Add(node.id,node);
            }
        }

        public ToolNode GetStartNode()
        {
            StartNode[] startNodes = Nodes.OfType<StartNode>().ToArray();

            if(startNodes.Length == 0)
            {
                Debug.LogError($"{this.name} found no startNode");
                return null;
            }

            if(startNodes.Length > 1){
                Debug.LogError($"{this.name} found multiple startNodes");
                return null;
            }

            return startNodes[0];
        }

        public ToolNode GetNode(string nextNodeId)
        {
            if(m_nodeDictionary.TryGetValue(nextNodeId, out ToolNode node))
            {
                return node;
            }
            return null;
        }

        public ToolNode GetNodeFromOutput(string outputNodeId, int index)
        {
            foreach (ToolConnection connection in m_connections)
            {
                if(connection.outputPort.nodeId == outputNodeId && connection.outputPort.portIndex == index)
                {
                    string nodeId = connection.inputPort.nodeId;
                    ToolNode inputNode = m_nodeDictionary[nodeId];
                    return inputNode;
                }
            }

            return null;
        }
    }
}
