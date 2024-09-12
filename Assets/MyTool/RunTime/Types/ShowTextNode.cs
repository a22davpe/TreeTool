using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using TMPro;

namespace MyTool
{
    [NodeInfo("ShowText", "Text/ShowText")]
    public class ShowTextNode : ToolNode
    {
        [ExposedProperty]
        public int SpeakerNumber=1;

        [TextArea]
        [ExposedProperty]
        public string Text;

        public override void OnEnableNode(ToolAsset currentTool, ToolObject toolObject)
        {
            currentTool.speechBubbles[SpeakerNumber - 1].text = Text;

            base.OnEnableNode(currentTool, toolObject);
        }

        public override void OnPlayerHasClicked(ToolAsset currentTool, ToolObject toolObject, int buttonIndex)
        {
            base.OnPlayerHasClicked(currentTool, toolObject, buttonIndex);
            OnProcess(currentTool,toolObject);
        }

        public override string OnProcess(ToolAsset currentTool, ToolObject toolObject)
        {
            return base.OnProcess(currentTool, toolObject);
        }
    }
}
