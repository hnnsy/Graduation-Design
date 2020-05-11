using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialsActive : MonoBehaviour
{
    private void Awake()
    {
        if (GlobalVar.tutorialsOver)
        {
            gameObject.SetActive(false);
        }
    }
}
