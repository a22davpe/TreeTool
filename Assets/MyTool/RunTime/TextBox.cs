using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyTool
{
    public class TextBox : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] EventPort onPlayerHasClicked;
        public void OnPointerClick(PointerEventData eventData)
        {
            onPlayerHasClicked.Invoke();
        }
    }
}
