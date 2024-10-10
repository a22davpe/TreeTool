using Codice.CM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

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

        protected string m_title;

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


        #region Runtimefunctions
        /// <summary>
        /// Runs when entering this node
        /// </summary>
        /// <param name="currentTool"></param>
        /// <param name="toolObject"></param>
        public virtual void OnEnterNode(ToolAsset currentTool, ToolObject toolObject){}

        /// <summary>
        /// Runs when player clicks on a textbox or clicks on allocated buttons
        /// </summary>
        /// <param name="currentTool"></param>
        /// <param name="toolObject"></param>
        /// <param name="buttonIndex"></param>
        public virtual void OnPlayerHasClicked(ToolAsset currentTool, ToolObject toolObject, int buttonIndex) { }

        /// <summary>
        /// Function that makes you exit this nodes and moves to new node. It returns the new nodes GUID. 
        /// If next node is null it closes down the toolObject
        /// </summary>
        /// <param name="currentTool"></param>
        /// <param name="toolObject"></param>
        /// <returns></returns>
        public virtual string ExitNode(ToolAsset currentTool, ToolObject toolObject)
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
        #endregion

        #region Editorfunctions

        /// <summary>
        /// Draws the editor for the node
        /// </summary>
        /// <param name="editorNode"></param>
        public virtual void Draw(ToolEditorNode editorNode)
        {

            //Checks if a title is saved
            if (String.IsNullOrEmpty(m_title))
                m_title = editorNode.name;

            
            TextField title = new TextField(30, true, false, '3')
            {
                value =  m_title
            };

            //If title is changed save the new value
            title.RegisterValueChangedCallback(evt =>
            {
                m_title = evt.newValue;
            });

            //Replaced the old title
            editorNode.title = m_title;
            editorNode.titleContainer.Clear();
            editorNode.titleContainer.Add(title);
        }
        #endregion
    }
}
