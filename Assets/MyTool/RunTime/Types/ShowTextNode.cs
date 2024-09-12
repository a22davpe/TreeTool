using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using TMPro;

namespace MyTool
{
    [NodeInfo("ShowText", "Text/SowText")]
    public class ShowTextNode : ToolNode
    {
        [ExposedProperty]
        public int SpeakerNumber=1;
        [ExposedProperty, TextArea]
        public string Text;

        public override void OnEnableNode(ToolAsset currentTool, ToolObject toolObject)
        {
            currentTool.speechBubbles[SpeakerNumber - 1].text = Text;

            base.OnEnableNode(currentTool, toolObject);
        }

        public override void OnPlayerHasClicked(ToolAsset currentTool, ToolObject toolObject)
        {
            base.OnPlayerHasClicked(currentTool, toolObject);
            OnProcess(currentTool,toolObject);
        }

        public override string OnProcess(ToolAsset currentTool, ToolObject toolObject)
        {
            return base.OnProcess(currentTool, toolObject);
        }
    }
}
