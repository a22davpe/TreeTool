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
        private bool m_hasFlowInput;
        private bool m_hasFlowOutput;

        public string title => m_nodeTitle;
        public string menuItem => m_menuItem;

        public bool hasFlowInput => m_hasFlowInput;
        public bool hasFlowOutput => m_hasFlowOutput;


        public NodeInfoAttribute(string title, string menuItem = "", bool hasFlowInput = true, bool hasFlowOutput = true)
        {
            m_menuItem = menuItem;
            m_nodeTitle = title;
            m_hasFlowInput = hasFlowInput;
            m_hasFlowOutput = hasFlowOutput;
        }
    }
}
