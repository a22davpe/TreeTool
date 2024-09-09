using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTool
{
    [CreateAssetMenu(menuName = "MyTool/New Tree")]
    public class ToolAsset : ScriptableObject
    {
        [SerializeReference]
        private List<ToolNode> m_nodes;

        public List<ToolNode> Nodes => m_nodes;

        public ToolAsset()
        {
            m_nodes = new List<ToolNode>();
        }
    }
}
