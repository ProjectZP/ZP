using UnityEngine;
using UnityEngine.Events;

namespace ZP.BHS.Zombie
{
    public class ZombieDamageController : MonoBehaviour //Todo:
    {
        public UnityEvent<float> OnGetDamaged;

        private void Awake()
        {
            OnGetDamaged?.AddListener(asdf);
        }

        private void asdf(float dummy) //Todo:
        {
            if (dummy > 0)
            {
                GetComponent<ZombieStateController>().ChangeZombieState(ZombieStates.ZombieDead);
            }
        }

        public void CaluateDamage(float damage)
        {

        }
    }
}
