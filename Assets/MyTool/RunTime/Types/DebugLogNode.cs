using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTool
{
    [NodeInfo("Debug Log", "Debug/Debug Log")]
    public class DebugLogNode : ToolNode
    {
        [ExposedProperty(), TextArea]
        public string logMessage;

        public override string OnProcess(ToolAsset currentTool)
        {
            Debug.Log(logMessage);

            return base.OnProcess(currentTool);
        }
    }
}
