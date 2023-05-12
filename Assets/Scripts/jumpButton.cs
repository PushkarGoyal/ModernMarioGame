using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class jumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] UiManager uiReference;

    public void OnPointerDown(PointerEventData eventData)
    {
        uiReference.jump = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        uiReference.jump = false;
    }

}
