using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTool
{
    [NodeInfo("Start", "Process/Start","")]
    public class StartNode : ToolNode
    {
        public override void OnEnterNode(ToolAsset currentTool, ToolObject toolObject)
        {
            base.OnEnterNode(currentTool, toolObject);

            Debug.Log("Start");

            ExitNode(currentTool, toolObject);
        }

        public override string ExitNode(ToolAsset currentTool, ToolObject toolObject)
        {
            return base.ExitNode(currentTool,toolObject);
        }

        public override void Draw(ToolEditorNode editorNode)
        {
            base.Draw(editorNode);

            editorNode.CreateInputPort("Input");
        }
    }
}
