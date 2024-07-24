using UnityEngine;
using UnityEngine.Events;

namespace ZP.BHS.Zombie
{
    public class ZombieDamageController : MonoBehaviour //Todo:
    {
        public UnityEvent<float> OnGetDamaged;

        ZombieStateController ZombieStateController;

        [SerializeField] GameObject dummyeffect;

        private void Start()
        {
            OnGetDamaged?.AddListener(CalcuateDamage);
        }


        public void CalcuateDamage(float damage)
        {
            //GameObject madeinstance = Instantiate(dummyeffect);
            //madeinstance.transform.localScale = Vector3.one * damage * 0.1f;

            Debug.Log("event act");

            //GetComponentInParent<ZombieStateController>().ChangeZombieState(ZombieStates.ZombieDead);

            Debug.Log(damage);

            if (damage > 100)
            {
                GetComponentInParent<ZombieStateController>().ChangeZombieState(ZombieStates.ZombieDead);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Debug.Log(collision.gameObject.name +"collider");

            //if(collision.gameObject.layer == 10)
            //{
            //    Debug.Log("muri appa");
            //    GetComponentInParent<ZombieStateController>().ChangeZombieState(ZombieStates.ZombieDead);
            //}
        }
    }
}
