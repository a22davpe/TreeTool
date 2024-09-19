using Codice.CM.Common;
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

        protected int outputIndex = 0;

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

        public virtual void OnEnableNode(ToolAsset currentTool, ToolObject toolObject){}

        public virtual void OnPlayerHasClicked(ToolAsset currentTool, ToolObject toolObject, int buttonIndex) { }

        //Returns the next node Id 
        public virtual string OnProcess(ToolAsset currentTool, ToolObject toolObject)
        {
            ToolNode nextNode = currentTool.GetNodeFromOutput(m_guid, outputIndex);

            if (nextNode == null)
            { 
                toolObject.EndDialogue();
                return null;
            }

            toolObject.MoveToNewNode(nextNode.id);

            return nextNode.id;
        }
    }
}
