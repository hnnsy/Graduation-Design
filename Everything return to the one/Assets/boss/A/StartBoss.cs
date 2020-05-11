using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartBoss : MonoBehaviour
{
    public GameObject bossTimeLine;
    public GameObject bossstarteffect;
    private void OnTriggerExit2D(Collider2D other)
    {
        AudioManager.Instance.ChangeBackgroundSound("academy_boss");

        bossTimeLine.GetComponent<PlayableDirector>().Play();
        
        gameObject.SetActive(false);
    }
}
