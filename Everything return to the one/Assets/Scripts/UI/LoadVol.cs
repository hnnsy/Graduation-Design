using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadVol : MonoBehaviour
{
    public GameObject master;
    public GameObject effect;
    public GameObject background;
    public GameObject enviorment;
    public GameObject character;

    private void Awake()
    {
        master.GetComponent<Slider>().value = GlobalVar.MasterVol;
        effect.GetComponent<Slider>().value = GlobalVar.EffectVol;
        background.GetComponent<Slider>().value = GlobalVar.BackgroundVol;
        enviorment.GetComponent<Slider>().value = GlobalVar.EnviormentVol;
        character.GetComponent<Slider>().value = GlobalVar.CharacterVol;
    }
}
