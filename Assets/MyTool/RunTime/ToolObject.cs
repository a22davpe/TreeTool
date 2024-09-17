using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace MyTool
{
    public class ToolObject : MonoBehaviour
    {
        [SerializeField]
        ToolAsset toolAsset;
        public TMP_Text MainText;
        public List<TMP_Text> optionsTexts;

        private string m_currentNodeId;
        private string m_nextNodeId;

        private ToolAsset toolInstance;

        public IntEventPort onPlayerClickEvent; 

        private void OnEnable()
        {
            onPlayerClickEvent.OnInvoked += OnPlayerClick;
            toolInstance = Instantiate(toolAsset);
            ExecuteAsset();
        }

        private void OnDisable()
        {
            onPlayerClickEvent.OnInvoked -= OnPlayerClick;
        }

        private void ExecuteAsset()
        {
            toolInstance.CreateNodeDictionary();

            ToolNode startNode = toolInstance.GetStartNode();
            MoveToNewNode(startNode.id);
        }

        
        private void EnableNewNode(string nodeId)
        {
            toolInstance.GetNode(m_currentNodeId).OnEnableNode(toolInstance, this);
        }

        public void ProcessCurrentNode()
        {
            m_nextNodeId = toolInstance.GetNode(m_currentNodeId).OnProcess(toolInstance, this);
        }

        public void MoveToNewNode()
        {
            m_currentNodeId = m_nextNodeId;
            EnableNewNode(m_currentNodeId);
        }

        public void MoveToNewNode(string NodeId)
        {
            m_currentNodeId = NodeId;
            EnableNewNode(m_currentNodeId);
        }

        private void OnPlayerClick(int buttonIndex)
        {
            toolInstance.GetNode(m_currentNodeId).OnPlayerHasClicked(toolInstance, this, buttonIndex);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                OnPlayerClick(0);

            if (Input.GetKeyDown(KeyCode.Alpha1))
                OnPlayerClick(1);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                OnPlayerClick(2);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                OnPlayerClick(3);
        }


        private void ProcessAndMoveToNextNode(ToolNode startNode)
        { 

            string nextNodeId = startNode.OnProcess(toolInstance, this);

            if (!string.IsNullOrEmpty(nextNodeId))
            {
                ToolNode node = toolInstance.GetNode(nextNodeId);

                ProcessAndMoveToNextNode(node);
            }
        }
    }
}
