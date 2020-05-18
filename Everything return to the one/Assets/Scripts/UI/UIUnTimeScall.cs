using UnityEngine;

namespace UI
{
    public class UIUnTimeScall : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        }
    }
}
