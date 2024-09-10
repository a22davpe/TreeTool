using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTool
{
    [Serializable]
    public class ToolNode
    {
        [SerializeField]
        private string m_guid;
        [SerializeField]
        private Rect m_position;

        public string typeName;

        public string id => m_guid;
        public Rect position => m_position;

        public ToolNode()
        {
            NewGUID();
        }

        private void NewGUID()
        {
            m_guid = Guid.NewGuid().ToString();
        }

        public void SetPosition(Rect position)
        {
            m_position = position;
        }

        //Returns the next node Id 
        public virtual string OnProcess(ToolAsset currentTool)
        {
            ToolNode nextNode = currentTool.GetNodeFromOutput(m_guid, 0);

            if (nextNode != null)
                return nextNode.id;
            return string.Empty;
        }
    }
}
