using UnityEngine;
using UnityEngine.Events;

namespace ZP.BHS.Zombie
{
    public class ZombieDamageController : MonoBehaviour //Todo:
    {
        public UnityEvent<float> OnGetDamaged;

        ZombieStateController ZombieStateController;

        private void Awake()
        {
            OnGetDamaged?.AddListener(CalcuateDamage);

        }

        public void CalcuateDamage(float damage)
        {
            Debug.Log(damage);
            if(damage > 5)
            {
                GetComponentInParent<ZombieStateController>().ChangeZombieState(ZombieStates.ZombieDead);
            }
        }
    }
}
