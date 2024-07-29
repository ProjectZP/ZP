using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZP.BHS.Zombie
{
    public class ZombieDamageController : MonoBehaviour //Todo:
    {
        public UnityEvent<float> OnGetDamaged;

        ZombieStateController ZombieStateController;

        private List<GameObject> StabbedWeapon = new List<GameObject>(8);


        private void Awake()
        {
            ZombieStateController = GetComponentInParent<ZombieStateController>();
        }

        private void Start()
        {
            OnGetDamaged?.AddListener(CalcuateDamage);
        }


        public void CalcuateDamage(float damage)
        {
            if (damage > 100)
            {
                ZombieStateController.ChangeZombieState(ZombieStates.ZombieDead);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            //if (collision.gameObject.layer == 1 << LayerMask.NameToLayer("Weapon"))
            //{
            //    if (ZombieStateController.currentZombieState == ZombieStates.ZombieDead)
            //    {
            //        FixedJoint joint = collision.gameObject.AddComponent<FixedJoint>();
            //        joint.connectedBody = this.GetComponent<Rigidbody>();
            //    }
            //}
        }

        private void OnCollisionExit(Collision collision)
        {
            //if (collision.gameObject.layer == 1 << LayerMask.NameToLayer("Weapon"))
            //{
            //    StabbedWeapon.Add(collision.gameObject);
            //}
        }
    }
}
