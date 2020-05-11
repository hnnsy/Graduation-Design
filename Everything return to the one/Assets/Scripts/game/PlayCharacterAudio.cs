using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCharacterAudio : MonoBehaviour
{
    public string AudioName;
    private void OnEnable()
    {
        AudioManager.Instance.StopCharacterAudio();
        AudioManager.Instance.PlayCharacterAudio(AudioName);
    }
}
