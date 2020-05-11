using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAF : MonoBehaviour
{
    private Vector2 ContactPoint;
    private Collider2D coll;
    public float attackPowerX,attackPowerY;
    public float demage;

    void Awake() {
        coll = GetComponent<Collider2D>();
        
    }

    void OnTriggerEnter2D(Collider2D other) {
         if(other.tag != "Untagged" && other.tag != "EnemyAF"){
            Vector2 difference = (transform.parent.position - transform.position).normalized;
            gameObject.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(difference.x * 2000,150));
         }
        if(other.tag == "EnemyAF")
        {
            gameObject.GetComponentInParent<PlayerControl>().damageable = false;
            Invoke(nameof(enddemagedisable),0.5f);
            AudioManager.Instance.PlayAudio("sword_hit_reject");
            FindObjectOfType<HitStop>().Stop(0.2f);
            
        }
        if(other.tag == "ground" || other.tag == "wall"){
            AudioManager.Instance.PlayAudio("sword_hit_window_4");
        }
    }
    
    void enddemagedisable(){
        gameObject.GetComponentInParent<PlayerControl>().damageable = true;
    }
    
}
