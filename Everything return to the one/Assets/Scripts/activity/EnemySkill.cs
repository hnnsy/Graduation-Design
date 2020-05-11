using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{
    public float demage;
    

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("PlayerAF")){
            
        }
    }

    public void bossASkill()
    {
        AudioManager.Instance.PlayAudio("bossAskill");
    }
}
