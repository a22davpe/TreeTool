using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MyTool
{
    [NodeInfo("DialogueBranch", "Text/Dialogue Branch",1,3)]
    public class DialogueBranch : ToolNode
    {
        [ExposedProperty]
        public int SpeakerNumber;
        [TextArea,ExposedProperty]
        public string MainText;
        
        [ExposedProperty, TextArea]
        public string Option1Text;

        [ExposedProperty, TextArea]
        public string Option2Text;

        float uwu;

        public override void OnEnableNode(ToolAsset currentTool, ToolObject toolObject)
        {
            currentTool.speechBubbles[SpeakerNumber].text = MainText;

            base.OnEnableNode(currentTool, toolObject);
        }


        public override void OnPlayerHasClicked(ToolAsset currentTool, ToolObject toolObject, int buttonIndex)
        {
            if (buttonIndex == 0)
                return;
            base.OnPlayerHasClicked(currentTool, toolObject, buttonIndex);
            outputIndex = buttonIndex-1;
            OnProcess(currentTool,toolObject);
        }


    }
}
