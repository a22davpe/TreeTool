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
        public  TMPro.TMP_Text textObject;
        [ExposedProperty, TextArea]
        public string Text;

        public override string OnProcess(ToolAsset currentTool)
        {
            Debug.Log(textObject);
            textObject.text = Text; 

            return base.OnProcess(currentTool);
        }
    }
}
