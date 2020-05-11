using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorBoosA : MonoBehaviour
{
    public GameObject entrance;
    public GameObject exit;
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            entrance.SetActive(true);
            exit.SetActive(true);
            gameObject.SetActive(false);
            AudioManager.Instance.PlayAudio("jiji_door_close");
        }
    }
}
