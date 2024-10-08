using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTool
{
    [NodeInfo("Debug Log", "Debug/Debug Log", "Writes a message in the console when the node is reached")]
    public class DebugLogNode : ToolNode
    {
        [ExposedProperty(), TextArea]
        public string logMessage;

        public override void OnEnableNode(ToolAsset currentTool, ToolObject toolObject)
        {
            base.OnEnableNode(currentTool, toolObject);

            Debug.Log(logMessage);

            OnProcess(currentTool, toolObject);
        }
    }
}
