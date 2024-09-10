using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace MyTool
{
    [NodeInfo("ShowText", "Text/SowText")]
    public class ShowTextNode : ToolNode
    {
        [ExposedProperty, TextArea]
        public string Text;
    }
}
