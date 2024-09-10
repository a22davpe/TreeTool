using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTool
{
    public class ToolObject : MonoBehaviour
    {
        [SerializeField]
        ToolAsset toolAsset;

        private ToolAsset toolInstance;

        private void OnEnable()
        {
            toolInstance = Instantiate(toolAsset);
            ExecuteAsset();
        }

        private void ExecuteAsset()
        {
            toolInstance.CreateNodeDictionary();

            ToolNode startNode = toolInstance.GetStartNode();
            ProcessAndMoveToNextNode(startNode);
        }

        private void ProcessAndMoveToNextNode(ToolNode startNode)
        {
            string nextNodeId = startNode.OnProcess(toolInstance);

            if (!string.IsNullOrEmpty(nextNodeId))
            {
                ToolNode node = toolInstance.GetNode(nextNodeId);

                ProcessAndMoveToNextNode(node);
            }
        }
    }
}
