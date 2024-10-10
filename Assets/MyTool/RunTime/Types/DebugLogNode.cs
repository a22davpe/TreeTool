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

        public override void OnEnterNode(ToolAsset currentTool, ToolObject toolObject)
        {
            base.OnEnterNode(currentTool, toolObject);

            Debug.Log(logMessage);

            ExitNode(currentTool, toolObject);
        }
    }
}
