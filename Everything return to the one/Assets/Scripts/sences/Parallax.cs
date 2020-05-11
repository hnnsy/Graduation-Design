using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public float moveRate;
    private float startPointX,startPointY;
    public bool LockY;
    private float camX;

    void Start()
    {
        camX = cam.position.x;
        startPointX = transform.position.x;
        startPointY = transform.position.y;
    }

     void Update()
     {
         if (!(camX == cam.position.x))
         {
             camX = cam.position.x;
             if (LockY) {
                 transform.position = new Vector2(startPointX + cam.position.x * moveRate, transform.position.y);
             } else if (!LockY) {
                 transform.position = new Vector2(startPointX + cam.position.x * moveRate, startPointY + cam.position.y * moveRate);
             }
         }
    }
}
