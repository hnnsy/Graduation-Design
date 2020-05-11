using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBlack : MonoBehaviour
{
    public GameObject blackScreen;
    public void clearToBlack()
    {
        blackScreen.SetActive(true);
    }

    public void blackToClear()
    {
        
    }

    public void blackScreenOff()
    {
        blackScreen.SetActive(false);
    }
}
