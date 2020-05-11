using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject mark;

    private void Awake()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mark.SetActive(true);
        AudioManager.Instance.PlayAudio("button");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mark.SetActive(false);
    }
    
}
