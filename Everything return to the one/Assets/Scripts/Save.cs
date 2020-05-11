using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    //进入出口位置
    public string EnterPosition;

    //保存点所在场景
    public string savePointScence = "Level1";

    //玩家属性
    public float playerMoveSpeed = -1;
    public float playerAttackSpeed = -1;
    public float playerMaxHP = -1;
    public float playerHP = -1;
    public float playerDamage = -1;

    //键位
    public KeyCode interactiveKey = KeyCode.DownArrow;

    public KeyCode jumpkey = KeyCode.C;
    public KeyCode attackkey = KeyCode.X;
    public KeyCode dashKey = KeyCode.Z;

    public KeyCode leftArrowKey = KeyCode.LeftArrow;
    public KeyCode rightArrowKey = KeyCode.RightArrow;
    
    public KeyCode menuKey = KeyCode.Escape;

    //音量
    public float MasterVol = 1;
    public float EffectVol = 1;
    public float BackgroundVol = 1;
    public float EnviormentVol = 1;
    public float CharacterVol = 1;

    //收集
    public bool nailAGet = false;
    
    //事件触发
    public bool tutorialsOver = false;//新手教程是否完成
    public bool BOSSAdefeat = false;//bossA是否击败
}
