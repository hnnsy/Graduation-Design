using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOffSelf : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(selfActiveOff());
    }

    IEnumerator selfActiveOff()
    {
        yield return new WaitForSeconds(2f);
        
        gameObject.SetActive(false);
    }

    public void desSelf()
    {
        Destroy(gameObject);
    }
}
