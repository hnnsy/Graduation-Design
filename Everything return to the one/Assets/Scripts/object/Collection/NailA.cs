using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NailA : MonoBehaviour
{
    public GameObject nail;
    public GameObject getnail;
    
    private void Update()
    {
        if (Input.GetKeyDown(GlobalVar.interactiveKey))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().attackEffect.GetComponent<PlayerAF>().demage += 1;
            GlobalVar.nailAGet = true;
            getnail.SetActive(true);
            AudioManager.Instance.PlayAudio("nailget");
            nail.SetActive(false);
        }
    }

}
