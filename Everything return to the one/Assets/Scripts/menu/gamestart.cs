using System.Collections;
using Gamekit2D;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class gamestart : MonoBehaviour
{
    public GameObject menu;
    public GameObject musicoption;
    public GameObject keyboardoption;
    public GameObject mainButtonGroup;
    public GameObject optionButtonGroup;
    public AudioMixer am;
    
    public GameObject loadingScreen;
    public GameObject blackScreen;
    public Slider slider;
    
    
    public void StartGame() {
        blackScreen.SetActive(true);
        AudioManager.Instance.PlayAudio("ui_button_confirm");
        StartCoroutine(LoadingLevelI());
    }
    public void ExitGame() {
        Application.Quit();
    }

    public void UIEnable() {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
    }

    //区域切换效果
    public void enterZone(string scenceName)
    {    //判断要切换到的场景在哪个区域
        if (GlobalVar.menuZone.Contains(GlobalVar.savePointScence))
        {
            //判断要切换的 场景是否和当前场景为不同区域
            if (!GlobalVar.menuZone.Contains(scenceName))
            {
                AudioManager.Instance.ChangeBackgroundSound("Title");
            }
        }
        else if (GlobalVar.blackZone.Contains(GlobalVar.savePointScence))
        {
            if (!GlobalVar.blackZone.Contains(scenceName))
            {
                AudioManager.Instance.ChangeBackgroundSound("WhitePalace");
            }
        }
        else if (GlobalVar.blackZoneBoss.Contains(GlobalVar.savePointScence))
        {
            if (!GlobalVar.blackZoneBoss.Contains(scenceName))
            {
                AudioManager.Instance.ChangeBackgroundSound("academy");
            }
        }
        else if (GlobalVar.redZone.Contains(GlobalVar.savePointScence))
        {
            if (!GlobalVar.redZone.Contains(scenceName))
            {
                AudioManager.Instance.ChangeBackgroundSound("level31");
            }
        }
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
    
    
    IEnumerator LoadingLevelI()
    {
        yield return new WaitForSeconds(1f);
        
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(GlobalVar.savePointScence);
        
        
        while (!operation.isDone)
        {
            slider.value = operation.progress;
            enterZone(SceneManager.GetActiveScene().name);
            yield return null;
        }
    }
}
