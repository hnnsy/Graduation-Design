using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Audio;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager Instance;

    private void Start()
    {
        Instance = this;
    }

    public Save creatSave()
    {
        Save save = new Save();
        
        save.EnterPosition = GlobalVar.EnterPosition;
        save.savePointScence = GlobalVar.savePointScence;

        save.playerMoveSpeed = GlobalVar.playerMoveSpeed;
        save.playerAttackSpeed = GlobalVar.playerAttackSpeed;
        save.playerMaxHP = GlobalVar.playerMaxHP;
        save.playerHP = GlobalVar.playerHP;
        save.playerDamage = GlobalVar.playerDamage;

        save.leftArrowKey = GlobalVar.leftArrowKey;
        save.rightArrowKey = GlobalVar.rightArrowKey;
        save.interactiveKey = GlobalVar.interactiveKey;
        save.jumpkey = GlobalVar.jumpkey;
        save.attackkey = GlobalVar.attackkey;
        save.dashKey = GlobalVar.dashKey;
        save.menuKey = GlobalVar.menuKey;

        save.MasterVol = GlobalVar.MasterVol;
        save.EffectVol = GlobalVar.EffectVol;
        save.BackgroundVol = GlobalVar.BackgroundVol;
        save.EnviormentVol = GlobalVar.BackgroundVol;
        save.CharacterVol = GlobalVar.CharacterVol;

        save.nailAGet = GlobalVar.nailAGet;

        save.tutorialsOver = GlobalVar.tutorialsOver;
        save.BOSSAdefeat = GlobalVar.BOSSAdefeat;
        
        return save;
    }

    public void SaveGameData()
    {
        Save save = creatSave();
        print(Application.persistentDataPath);
        if (!Directory.Exists(Application.persistentDataPath+"/game_SaveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_SaveData");
        }
        
        BinaryFormatter formatter = new BinaryFormatter();//序列化数据
        FileStream file = File.Create(Application.persistentDataPath + "/game_SaveData/save.hnsy");//创建一个存档文件
        formatter.Serialize(file,save);//将存档物体的以序列化数据形式存入到存档文件
        file.Close();
    }

    public void LoadGameData()
    {
        if (File.Exists(Application.persistentDataPath + "/game_SaveData/save.hnsy"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + "/game_SaveData/save.hnsy",FileMode.Open);
            Save save = bf.Deserialize(fileStream) as Save;
            fileStream.Close();
            LoadGameData(save);
            LoadAfterDo();
        }
    }

    public void LoadGameData(Save save)
    {
        GlobalVar.EnterPosition = save.EnterPosition;
        GlobalVar.savePointScence = save.savePointScence;

        GlobalVar.playerMoveSpeed = save.playerMoveSpeed;
        GlobalVar.playerAttackSpeed = save.playerAttackSpeed;
        GlobalVar.playerMaxHP = save.playerMaxHP;
        GlobalVar.playerHP = save.playerHP;
        GlobalVar.playerDamage = save.playerDamage;

        GlobalVar.leftArrowKey = save.leftArrowKey;
        GlobalVar.rightArrowKey = save.rightArrowKey;
        GlobalVar.interactiveKey = save.interactiveKey;
        GlobalVar.jumpkey = save.jumpkey;
        GlobalVar.attackkey = save.attackkey;
        GlobalVar.dashKey = save.dashKey;
        GlobalVar.menuKey = save.menuKey;

        GlobalVar.MasterVol = save.MasterVol;
        GlobalVar.EffectVol = save.EffectVol;
        GlobalVar.BackgroundVol = save.BackgroundVol;
        GlobalVar.BackgroundVol = save.EnviormentVol;
        GlobalVar.CharacterVol = save.CharacterVol;

        GlobalVar.nailAGet = save.nailAGet;

        GlobalVar.tutorialsOver = save.tutorialsOver;
        GlobalVar.BOSSAdefeat = save.BOSSAdefeat;
    }

    public void LoadAfterDo()
    {
        AudioManager.Instance.initGameVol();
    }

}
