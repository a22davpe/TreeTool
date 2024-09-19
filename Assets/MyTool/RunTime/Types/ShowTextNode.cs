using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using TMPro;

namespace MyTool
{
    [NodeInfo("ShowText", "Text/ShowText", "Puts text in the main text field and waits on plyer input")]
    public class ShowTextNode : ToolNode
    {


        [TextArea]
        [ExposedProperty]
        public string Text;

        public override void OnEnableNode(ToolAsset currentTool, ToolObject toolObject)
        {
            toolObject.MainText.text = Text;

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
