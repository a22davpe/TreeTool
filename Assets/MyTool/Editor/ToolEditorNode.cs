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

        public ToolNode Node => m_toolNode;

        private Port m_outputPort;

        private List<Port> m_ports;

        public ToolEditorNode(ToolNode node)
        {
            this.AddToClassList("code-graph-node");

            m_toolNode = node;

            Type typeInfo = node.GetType();
            NodeInfoAttribute info = typeInfo.GetCustomAttribute<NodeInfoAttribute>();

            title = info.title;

            m_ports = new List<Port>();

            string[] depths = info.menuItem.Split('/');
            foreach (string depth in depths)
            {
                this.AddToClassList(depth.ToLower().Replace(' ', '-'));
            }

            this.name = typeInfo.Name;
            
            if(info.hasFlowInput)
                CreateFlowInputPort();

            if (info.hasFlowOutput)
                CreateFlowOutputPort();
        }

        private void CreateFlowOutputPort()
        {
            m_outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(PortTypes.FlowPort));
            m_outputPort.portName = "Out";
            m_outputPort.tooltip = "The flow output";
            m_ports.Add(m_outputPort);
            outputContainer.Add(m_outputPort);
        }

        private void CreateFlowInputPort()
        {
            // 54:27
            m_outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(PortTypes.FlowPort));
            m_outputPort.portName = "Out";
            m_outputPort.tooltip = "The flow output";
            m_ports.Add(m_outputPort);
            outputContainer.Add(m_outputPort);
        }

        public void SavePosition()
        {
            m_toolNode.SetPosition(GetPosition());
        }
    }
}
