using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level21Light : MonoBehaviour
{
    public Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(cam.position.x,transform.position.y);
    }
}
