using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace MyTool
{
    [NodeInfo("DialogueBranch", "Text/Dialogue Branch","Puts text in the mainTextFeild and options in the options field and waits on plyer input",1,3)]
    public class DialogueBranch : ToolNode
    {

        [TextArea,ExposedProperty]
        public string MainText;
        
        [ExposedProperty, TextArea]
        public string Option1Text;

        [ExposedProperty, TextArea]
        public string Option2Text;

        [ExposedProperty, TextArea]
        public string Option3Text;


        public override void OnEnableNode(ToolAsset currentTool, ToolObject toolObject)
        {
            toolObject.MainText.text = MainText;

            if (!string.IsNullOrEmpty(Option1Text))
                EnableText(Option1Text, toolObject.optionsTexts[0]);

            if (!string.IsNullOrEmpty(Option1Text))
                EnableText(Option2Text, toolObject.optionsTexts[1]);

            if (!string.IsNullOrEmpty(Option1Text))
                EnableText(Option3Text, toolObject.optionsTexts[2]);

            base.OnEnableNode(currentTool, toolObject);
        }

        private void EnableText(string textString, TMP_Text textInstance)
        {
            textInstance.enabled = true;
            textInstance.text = textString;
        }

        private void DisableText(TMP_Text textInstance)
        {
            textInstance.enabled = false;
        }


        public override void OnPlayerHasClicked(ToolAsset currentTool, ToolObject toolObject, int buttonIndex)
        {
            if (buttonIndex == 0)
                return;
            base.OnPlayerHasClicked(currentTool, toolObject, buttonIndex);
            outputIndex = buttonIndex-1;
            
            OnProcess(currentTool,toolObject);
        }


        public override string OnProcess(ToolAsset currentTool, ToolObject toolObject)
        {
            if (!string.IsNullOrEmpty(Option1Text))
                DisableText(toolObject.optionsTexts[0]);

            if (!string.IsNullOrEmpty(Option1Text))
                DisableText(toolObject.optionsTexts[1]);

            if (!string.IsNullOrEmpty(Option1Text))
                DisableText(toolObject.optionsTexts[2]);


            return base.OnProcess(currentTool, toolObject);
        }


    }
}
