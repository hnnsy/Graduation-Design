using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFBOSSSHOW : MonoBehaviour
{
    private void Awake()
    {
        if (GlobalVar.BOSSAdefeat)
        {
            gameObject.SetActive(false);
        }
    }
}
