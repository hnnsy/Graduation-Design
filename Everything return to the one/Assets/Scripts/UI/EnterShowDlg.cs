using UnityEngine;

namespace UI
{
    public class EnterShowDlg : MonoBehaviour
    {
        public GameObject dialog;
    
        void OnTriggerEnter2D(Collider2D other) {
            if(other.tag == "Player"){
                dialog.SetActive(true);
            }
        }

        void OnTriggerExit2D(Collider2D other) {
            if(other.tag == "Player"){
                dialog.SetActive(false);
            }
        }
    }
}
