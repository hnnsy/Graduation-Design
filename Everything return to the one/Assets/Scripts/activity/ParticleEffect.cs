using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(death());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator death(){
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
