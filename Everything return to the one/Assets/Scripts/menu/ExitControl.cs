using System;
using System.Collections;
using System.Collections.Generic;
using Gamekit2D;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitControl : MonoBehaviour
{
    [Header("要切换到的场景名")]
    [SceneName]
    public string senceName;
    public string enterPosition;

    private void Awake()
    {
        if (!GlobalVar.tutorialsOver)
        {
            GlobalVar.tutorialsOver = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && other.GetComponent<PlayerControl>().hp > 0 && other.GetComponent<PlayerControl>().damageable)
        {
            saveExitData();
            EnterLevel(senceName);
            GlobalVar.EnterPosition = enterPosition;
            print("进入位置为:"+enterPosition);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player") && other.GetComponent<PlayerControl>().hp > 0 && other.GetComponent<PlayerControl>().damageable)
        {
            saveExitData();
            EnterLevel(senceName);
            GlobalVar.EnterPosition = enterPosition;
            print("进入位置为:"+enterPosition);
        }
    }


    public void saveExitData()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");


        GlobalVar.playerMaxHP = player.GetComponent<PlayerControl>().maxHP;


        GlobalVar.playerHP = player.GetComponent<PlayerControl>().hp;


        GlobalVar.playerAttackSpeed = player.GetComponent<PlayerControl>().attackSpeed;


        GlobalVar.playerMoveSpeed = player.GetComponent<PlayerControl>().moveSpeed;


        GlobalVar.playerDamage = player.GetComponent<PlayerControl>().attackEffect.GetComponent<PlayerAF>().demage;

        Debug.Log("最大生命值："+GlobalVar.playerMaxHP+",当前生命值:"+GlobalVar.playerHP+",攻击速度:"+GlobalVar.playerAttackSpeed+",移动速度:"+GlobalVar.playerMoveSpeed+",武器伤害:"+GlobalVar.playerDamage);
    }

    public void EnterLevel(string senceName) {
        enterZone(senceName);
        SceneManager.LoadScene(senceName);
    }

    //区域切换效果
    public void enterZone(string senceName)
    {    //判断要切换到的场景在哪个区域
        if (GlobalVar.menuZone.Contains(senceName))
        {
            //判断要切换的 场景是否和当前场景为不同区域
            if (!GlobalVar.menuZone.Contains(SceneManager.GetActiveScene().name))
            {
                AudioManager.Instance.ChangeBackgroundSound("Title");
            }
        }
        else if (GlobalVar.blackZone.Contains(senceName))
        {
            if (!GlobalVar.blackZone.Contains(SceneManager.GetActiveScene().name))
            {
                AudioManager.Instance.ChangeBackgroundSound("WhitePalace");
            }
        }
        else if (GlobalVar.blackZoneBoss.Contains(senceName))
        {
            if (!GlobalVar.blackZoneBoss.Contains(SceneManager.GetActiveScene().name))
            {
                AudioManager.Instance.ChangeBackgroundSound("academy");
            }
        }
        else if (GlobalVar.redZone.Contains(senceName))
        {
            if (!GlobalVar.redZone.Contains(SceneManager.GetActiveScene().name))
            {
                
            }
        }
    }
}
