using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTool
{
    [NodeInfo("Start", "Process/Start", 0)]
    public class StartNode : ToolNode
    {
        public override string OnProcess(ToolAsset currentTool)
        {
            Debug.Log("Start");

            return base.OnProcess(currentTool);
        }
    }
}
