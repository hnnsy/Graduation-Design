using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeKey : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public GameObject interactive;
    public GameObject jump;
    public GameObject attack;
    public GameObject dash;
    public GameObject menu;

    private void Awake()
    {
        refushKey();
    }

    public void refushKey()
    {
        left.GetComponentInChildren<TMP_Text>().text = GlobalVar.leftArrowKey.ToString();
        right.GetComponentInChildren<TMP_Text>().text = GlobalVar.rightArrowKey.ToString();
        interactive.GetComponentInChildren<TMP_Text>().text = GlobalVar.interactiveKey.ToString();
        jump.GetComponentInChildren<TMP_Text>().text = GlobalVar.jumpkey.ToString();
        attack.GetComponentInChildren<TMP_Text>().text = GlobalVar.attackkey.ToString();
        dash.GetComponentInChildren<TMP_Text>().text = GlobalVar.dashKey.ToString();
        menu.GetComponentInChildren<TMP_Text>().text = GlobalVar.menuKey.ToString();
    }
}
