using System.Collections;
using System.Collections.Generic;
using Gamekit2D;
using UnityEngine;

public class GlobalVar
{    
    [Header("进入出口位置")] public static string EnterPosition;

    [Header("保存点所在场景")] [SceneName] public static string savePointScence = "Level1";

    [Header("玩家属性")]
    public static float playerMoveSpeed = -1;
    public static float playerAttackSpeed = -1;
    public static float playerMaxHP = -1;
    public static float playerHP = -1;
    public static float playerDamage = -1;

    [Header("键位")]
    public static KeyCode interactiveKey = KeyCode.DownArrow;

    public static KeyCode jumpkey = KeyCode.C;
    public static KeyCode attackkey = KeyCode.X;
    public static KeyCode dashKey = KeyCode.Z;

    public static KeyCode leftArrowKey = KeyCode.LeftArrow;
    public static KeyCode rightArrowKey = KeyCode.RightArrow;
    
    public static KeyCode menuKey = KeyCode.Escape;

    [Header("音量")]
    public static float MasterVol = 1;
    public static float EffectVol = 1;
    public static float BackgroundVol = 1;
    public static float EnviormentVol = 1;
    public static float CharacterVol = 1;

    [Header("收集")]
    public static bool nailAGet = false;
    public static bool nailBGet = false;
    public static bool nailCGet = false;
    public static bool nailDGet = false;

    [Header("事件触发")]
    public static bool tutorialsOver = false;//新手教程是否完成

    public static bool BOSSAdefeat = false;
    
    [Header("区域")]
    public static List<string> menuZone = new List<string>(){"Level0"};
    public static List<string> blackZone = new List<string>(){"Level1","Level2_1","Level2_2","Level2_3","Level2_5","Level2_6"};
    public static List<string> blackZoneBoss = new List<string>(){"Level2_4"};
    public static List<string> redZone = new List<string>(){"Level3_2"};
}
