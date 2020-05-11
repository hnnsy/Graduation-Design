using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    //混音器
    public AudioMixer am;
    //声音播放组件
    private AudioSource effectPlayer;
    private AudioSource backgroundPlayer;
    private AudioSource enviormentPlayer;
    private AudioSource characterPlayer;

    void Start()
    {
        Instance = this;
        initGameVol();
        //获取音频播放组件
        effectPlayer = GetComponents<AudioSource>()[0];
        backgroundPlayer = GetComponents<AudioSource>()[1];
        enviormentPlayer = GetComponents<AudioSource>()[2];
        characterPlayer = GetComponents<AudioSource>()[3];
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (backgroundPlayer.volume < 1)
        {
            backgroundPlayer.volume+=0.01f;
        }
        
    }

    //初始化
    public void initGameVol()
    {
        am.SetFloat("masterVol",GlobalVar.MasterVol*80-80);
        am.SetFloat("effectVol",GlobalVar.EffectVol*80-80);
        am.SetFloat("backgroundVol",GlobalVar.BackgroundVol*80-80);
        am.SetFloat("enviormentVol",GlobalVar.EnviormentVol*80-80);
        am.SetFloat("characterVol",GlobalVar.CharacterVol*80-80);
    }

    //播放音效
    public void PlayAudio(string name)
    {
        //加载的音效
        AudioClip clip = Resources.Load<AudioClip>(name);
        //播放
        effectPlayer.PlayOneShot(clip);
    }
    //播放角色语音
    public void PlayCharacterAudio(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(name);
        characterPlayer.PlayOneShot(clip);
    }
    //停止播放角色语音
    public void StopCharacterAudio()
    {
        characterPlayer.Stop();
    }

    //更换背景音乐
    public void ChangeBackgroundSound(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(name);
        backgroundPlayer.clip = clip;
        backgroundPlayer.Play();
    }

    //停止播放背景音乐
    public void StopBackgroundSound()
    {
        backgroundPlayer.Stop();
    }
    //暂停播放背景音乐
    public void PauseBackgroundSound()
    {
        backgroundPlayer.Pause();
    }
    //继续背景音乐
    public void PlayBackgroundSound()
    {
        backgroundPlayer.volume = 0;
        backgroundPlayer.Play();
    }
    
    //更换环境音乐
    public void ChangeEnviormentSound(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(name);
        enviormentPlayer.clip = clip;
        enviormentPlayer.Play();
    }
    //暂停播放环境音乐
    public void PauseEnviormentSound()
    {
        enviormentPlayer.Pause();
    }
    //停止播放环境音乐
    public void StopEnviormentSound()
    {
        enviormentPlayer.Stop();
    }
    //继续环境音乐
    public void PlayEnviormentSound()
    {
        enviormentPlayer.Play();
    }

}
