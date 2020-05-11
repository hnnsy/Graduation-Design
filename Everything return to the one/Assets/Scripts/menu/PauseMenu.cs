using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject menu;
    public GameObject musicoption;
    public GameObject keyboardoption;
    public GameObject mainButtonGroup;
    public GameObject optionButtonGroup;
    public AudioMixer am;
    void Update()
    {
        if (Input.GetKeyDown(GlobalVar.menuKey))
        {
            if (!pauseMenu.activeSelf)
            {
                pauseGame();
            }else
            {
                continueGame();
            }

        }
    }
    
    public void backToMenu() {
        Time.timeScale = 1f;
        saveExitData();
        GameSaveManager.Instance.SaveGameData();
        SceneManager.LoadScene("Level0");
        AudioManager.Instance.ChangeBackgroundSound("Title");
    }
    
    public void saveExitData()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");


        GlobalVar.playerMaxHP = player.GetComponent<PlayerControl>().maxHP;


        GlobalVar.playerHP = player.GetComponent<PlayerControl>().hp;


        GlobalVar.playerAttackSpeed = player.GetComponent<PlayerControl>().attackSpeed;


        GlobalVar.playerMoveSpeed = player.GetComponent<PlayerControl>().moveSpeed;


        GlobalVar.playerDamage = player.GetComponent<PlayerControl>().attackEffect.GetComponent<PlayerAF>().demage;


        GlobalVar.EnterPosition = "s";
    }
    
    public void pauseGame() {
        AudioManager.Instance.PlayAudio("ui_button_confirm");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void continueGame() {
        Time.timeScale = 1f;
        
        menu.SetActive(true);
        keyboardoption.SetActive(false);
        musicoption.SetActive(false);
        
        mainButtonGroup.SetActive(true);
        optionButtonGroup.SetActive(false);
        
        pauseMenu.SetActive(false);
    }

    public void optionactive()
    {
        optionButtonGroup.SetActive(true);
        mainButtonGroup.SetActive(false);
    }
    
    public void mainactive()
    {
        mainButtonGroup.SetActive(true);
        optionButtonGroup.SetActive(false);
    }
    
    public void menuactive()
    {
        GameSaveManager.Instance.SaveGameData();
        menu.SetActive(true);
        keyboardoption.SetActive(false);
        musicoption.SetActive(false);
    }

    public void keyboardactive()
    {
        keyboardoption.SetActive(true);
        menu.SetActive(false);
    }
    
    public void musicactive()
        {
            musicoption.SetActive(true);
            menu.SetActive(false);
        }



    public void setMasterVol(float value) {
        am.SetFloat("masterVol",value*80-80);
        GlobalVar.MasterVol = value;
    }
    
    public void setEffectVol(float value) {
        am.SetFloat("effectVol",value*80-80);
        GlobalVar.EffectVol = value;
    }
    
    public void setBackgroundVol(float value) {
        am.SetFloat("backgroundVol",value*80-80);
        GlobalVar.BackgroundVol = value;
    }
    
    public void setEnviormentVol(float value) {
        am.SetFloat("enviormentVol",value*80-80);
        GlobalVar.EnviormentVol = value;
    }
    
    public void setCharacterVol(float value) {
        am.SetFloat("characterVol",value*80-80);
        GlobalVar.CharacterVol = value;
    }
}
