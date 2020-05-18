﻿using System;
using UnityEngine;

namespace UI.keyset
{
    public class JumpKetSet : MonoBehaviour
    {
        public GameObject keyboard;
        public GameObject showkey;
        public GameObject setkey;
        void Update()
        {
            if (setkey.activeInHierarchy && Input.anyKeyDown)
            {
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        GlobalVar.jumpkey = keyCode;
                        Debug.Log(keyCode.ToString());
                        keysetoff();
                        keyboard.GetComponent<ChangeKey>().refushKey();
                    }
                }
            }
        }

        public void keysetActive()
        {
            setkey.SetActive(true);
            showkey.SetActive(false);
        }
    
        public void keysetoff()
        {
            showkey.SetActive(true);
            setkey.SetActive(false);
        }
    }
}
