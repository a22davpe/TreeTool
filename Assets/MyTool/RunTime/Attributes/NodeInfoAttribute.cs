using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTool
{
    public class NodeInfoAttribute : Attribute
    {
        public string m_nodeTitle;
        private string m_menuItem;
        public int m_hasFlowInput;
        private int m_hasFlowOutput;

        public string title => m_nodeTitle;
        public string menuItem => m_menuItem;

        private string m_toolTip;

        public string toolTip => m_toolTip;


        public NodeInfoAttribute(string title, string menuItem = "", string toolTip = "")
        {
            m_menuItem = menuItem;
            m_nodeTitle = title;
            m_toolTip = toolTip;
        }
    

    }
}
