using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace MyTool.Editor
{
    public class ToolEditorNode : Node
    {
        private ToolNode m_toolNode;

        public ToolEditorNode(ToolNode node)
        {
            this.AddToClassList("code-graph-node");

            m_toolNode = node;

            Type typeInfo = node.GetType();
            NodeInfoAttribute info = typeInfo.GetCustomAttribute<NodeInfoAttribute>();

            title = info.title;

            string[] depths = info.menuItem.Split('/');
            foreach (string depth in depths)
            {
                this.AddToClassList(depth.ToLower().Replace(' ', '-'));
            }

            this.name = typeInfo.Name;
        }
    }
}
