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

        public string title => m_nodeTitle;
        public string menuItem => m_menuItem;

        public NodeInfoAttribute(string title, string menuItem = "")
        {
            m_menuItem = menuItem;
            m_nodeTitle = title;
        }
    }
}
