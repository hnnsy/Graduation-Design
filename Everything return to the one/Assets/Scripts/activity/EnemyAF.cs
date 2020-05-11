using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAF : MonoBehaviour
{
    public float demage;
    

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("PlayerAF")){
            
        }
    }
}
