using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUnTimeScall : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
    }
}
