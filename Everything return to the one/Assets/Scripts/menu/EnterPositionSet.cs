using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPositionSet : MonoBehaviour
{    [Header("玩家")]
    public Transform player;
    
    [Space]

    [Header("上左位置")] public Transform ul;
    [Header("上中位置")] public Transform um;
    [Header("上右位置")] public Transform ur;
    
    [Header("下左位置")] public Transform dl;
    [Header("下中位置")] public Transform dm;
    [Header("下右位置")] public Transform dr;
    
    [Header("左上位置")] public Transform lu;
    [Header("左中位置")] public Transform lm;
    [Header("左下位置")] public Transform ld;
    
    [Header("右上位置")] public Transform ru;
    [Header("右中位置")] public Transform rm;
    [Header("右下位置")] public Transform rd;
    [Header("保存点位置")] public Transform s;
    void Awake()
    {
        if (GlobalVar.EnterPosition =="ul")
        {
            player.position = ul.position;
        }else if (GlobalVar.EnterPosition =="um")
        {
            player.position = um.position;
        }else if (GlobalVar.EnterPosition =="ur")
        {
            player.position = ur.position;
        }else if (GlobalVar.EnterPosition =="dl")
        {
            player.position = dl.position;
        }else if (GlobalVar.EnterPosition =="dm")
        {
            player.position = dm.position;
        }else if (GlobalVar.EnterPosition =="dr")
        {
            player.position = dr.position;
        }else if (GlobalVar.EnterPosition =="lu")
        {
            player.position = lu.position;
        }else if (GlobalVar.EnterPosition =="lm")
        {
            player.position = lm.position;
        }else if (GlobalVar.EnterPosition =="ld")
        {
            player.position = ld.position;
        }else if (GlobalVar.EnterPosition =="ru")
        {
            player.position = ru.position;
        }else if (GlobalVar.EnterPosition =="rm")
        {
            player.position = rm.position;
        }else if (GlobalVar.EnterPosition =="rd")
        {
            player.position = rd.position;
        }else if (GlobalVar.EnterPosition =="s")
        {
            player.position = s.position;
        }
        else
        {
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
