using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTool
{
    [NodeInfo("Start", "Process/Start","", 0)]
    public class StartNode : ToolNode
    {
        public override void OnEnableNode(ToolAsset currentTool, ToolObject toolObject)
        {
            base.OnEnableNode(currentTool, toolObject);

            Debug.Log("Start");

            OnProcess(currentTool, toolObject);
        }

        public override string OnProcess(ToolAsset currentTool, ToolObject toolObject)
        {
            return base.OnProcess(currentTool,toolObject);
        }
    }
}
