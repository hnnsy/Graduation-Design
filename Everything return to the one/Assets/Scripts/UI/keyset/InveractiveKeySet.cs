using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InveractiveKeySet : MonoBehaviour
{
    public GameObject keyboard;
    public GameObject showkey;
    public GameObject setkey;
    void Update()
    {
        if (setkey.activeInHierarchy && Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    GlobalVar.interactiveKey = keyCode;
                    Debug.Log(keyCode.ToString());
                    keysetoff();
                    keyboard.GetComponent<ChangeKey>().refushKey();
                }
            }
        }
    }

    public void keysetActive()
    {
        setkey.SetActive(true);
        showkey.SetActive(false);
    }
    
    public void keysetoff()
    {
        showkey.SetActive(true);
        setkey.SetActive(false);
    }
}
