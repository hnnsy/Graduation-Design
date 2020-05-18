using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayHpBar : MonoBehaviour
    {
        private GameObject target;

        private void Awake()
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            GetComponent<Slider>().value =
                target.GetComponent<PlayerControl>().hp / target.GetComponent<PlayerControl>().maxHP;
        }
    }
}
