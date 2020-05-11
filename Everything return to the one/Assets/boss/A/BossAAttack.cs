using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAAttack : MonoBehaviour
{
    private Transform target;
    private Vector2 vt;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        vt = (target.position - transform.position).normalized;
        //new Vector3(target.position.x,target.position.y - 0.5f,target.position.z)
    }

    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = vt * 500;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "ground" || other.tag == "wall")
        {
            StartCoroutine(delself());
        }
    }

    IEnumerator delself()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
