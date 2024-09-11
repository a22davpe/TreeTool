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

        public override string OnProcess(ToolAsset currentTool)
        {
            currentTool.speechBubbles[SpeakerNumber-1].text = Text; 

            return base.OnProcess(currentTool);
        }
    }
}
