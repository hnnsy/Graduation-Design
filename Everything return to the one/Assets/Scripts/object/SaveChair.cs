using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveChair : MonoBehaviour
{
    public GameObject saveicon;
    
    void Update()
    {
        if (Input.GetKeyDown(GlobalVar.interactiveKey))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            
            GlobalVar.savePointScence = SceneManager.GetActiveScene().name;
            GlobalVar.EnterPosition = "s";
            GlobalVar.playerMaxHP = player.GetComponent<PlayerControl>().maxHP;
            GlobalVar.playerHP = player.GetComponent<PlayerControl>().hp;
            GlobalVar.playerAttackSpeed = player.GetComponent<PlayerControl>().attackSpeed;
            GlobalVar.playerMoveSpeed = player.GetComponent<PlayerControl>().moveSpeed;
            GlobalVar.playerDamage = player.GetComponent<PlayerControl>().attackEffect.GetComponent<PlayerAF>().demage;
            
            
            AudioManager.Instance.PlayAudio("ui_save");
            
            saveicon.SetActive(false);
            gameObject.SetActive(false);
            saveicon.SetActive(true);
            
            GameSaveManager.Instance.SaveGameData();
            
            Debug.Log("存档点设置为："+GlobalVar.savePointScence);
        }
    }
    

}
