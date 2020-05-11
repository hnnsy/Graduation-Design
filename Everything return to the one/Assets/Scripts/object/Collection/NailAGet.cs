using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class NailAGet : MonoBehaviour
{
    private void Awake()
    {
        
        if (GlobalVar.nailAGet)
        {
            gameObject.SetActive(false);
        }
    }
}
