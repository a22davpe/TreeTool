using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;

namespace MyTool.Editor
{
    
    public class ToolEditorNode : Node
    {
        private ToolNode m_toolNode;

        public ToolNode Node => m_toolNode;

        private Port m_outputPort;

        private List<Port> m_ports;
        private SerializedProperty m_serializedProperty;

        public List<Port> Ports => m_ports;

        private SerializedObject m_serializedObject;
        public ToolEditorNode(ToolNode node, SerializedObject ToolObject)
        {
            this.AddToClassList("code-graph-node");

            m_serializedObject = ToolObject; 
            m_toolNode = node;
            m_ports = new List<Port>();

            //Type typeInfo = node.GetType();
            //NodeInfoAttribute info = typeInfo.GetCustomAttribute<NodeInfoAttribute>();

            //title = info.title;

            //if (!string.IsNullOrEmpty(info.toolTip))
            //    tooltip = info.toolTip;


            //string[] depths = info.menuItem.Split('/');
            //foreach (string depth in depths)
            //{
            //    this.AddToClassList(depth.ToLower().Replace(' ', '-'));
            //}

            //this.name = typeInfo.Name;

            node.Draw(this);

            //foreach (FieldInfo property in typeInfo.GetFields())
            //{
            //    if(property.GetCustomAttribute<ExposedPropertyAttribute>() is ExposedPropertyAttribute exposedProperty){
                   
            //        PropertyField field =  DrawProperty(property.Name);                }
            //}



            RefreshExpandedState();
        }

        private PropertyField DrawProperty(string propertyName)
        {
            if(m_serializedProperty == null)
            {
                FetchSerializedProperty();
            }

            SerializedProperty prop = m_serializedProperty.FindPropertyRelative(propertyName);
            PropertyField field = new PropertyField(prop);
            field.bindingPath = prop.propertyPath;
            extensionContainer.Add(field);

            return field;
        }

        private void FetchSerializedProperty()
        {
            SerializedProperty nodes = m_serializedObject.FindProperty("m_nodes");

            if (nodes.isArray)
            {
                int size = nodes.arraySize;

                for (int i = 0; i < size; i++)
                {
                    var element = nodes.GetArrayElementAtIndex(i);
                    var elementId = element.FindPropertyRelative("m_guid");
                    if(elementId.stringValue == m_toolNode.id)
                    {
                        m_serializedProperty = element;
                    }
                }
            }
        }

        public void CreateFlowInputPort()
        {

            Port input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(PortTypes.FlowPort));
            input.portName = "Input";
            input.tooltip = "Flow input";
            m_ports.Add(input);
            inputContainer.Add(input);
        }

        public void CreateFlowOutputPort(string Name, string tooltip = "")
        {
            m_outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(PortTypes.FlowPort));
            m_outputPort.portName = "Output";
            m_outputPort.tooltip = "Flow output";
            m_ports.Add(m_outputPort);
            outputContainer.Add(m_outputPort);
        }

        public void SavePosition()
        {
            m_toolNode.SetPosition(GetPosition());
        }
    }
}
