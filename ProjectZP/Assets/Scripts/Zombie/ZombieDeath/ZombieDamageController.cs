using UnityEngine;
using UnityEngine.Events;

namespace ZP.BHS.Zombie
{
    public class ZombieDamageController : MonoBehaviour //Todo:
    {
        public UnityEvent<float> OnGetDamaged;

        ZombieStateController ZombieStateController;

        [SerializeField] GameObject dummyeffect;

        private void Awake()
        {
            OnGetDamaged?.AddListener(CalcuateDamage);
        }

        public void CalcuateDamage(float damage)
        {
            //GameObject madeinstance = Instantiate(dummyeffect);
            //madeinstance.transform.localScale = Vector3.one * damage * 0.1f;

            GetComponentInParent<ZombieStateController>().ChangeZombieState(ZombieStates.ZombieDead);

            //if(damage > 50)
            //{
            //    GetComponentInParent<ZombieStateController>().ChangeZombieState(ZombieStates.ZombieDead);
            //}
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.name == "Knife")
            {
                Instantiate(dummyeffect, collision.transform.position , collision.transform.rotation , null);
                GetComponentInParent<ZombieStateController>().ChangeZombieState(ZombieStates.ZombieDead);
            }
        }
    }
}
