using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZP.BHS.Zombie
{
    public class ZombieDamageController : MonoBehaviour //Todo:
    {
        public UnityEvent<float, GameObject> OnGetDamaged;

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


        GameObject attachedWeapon;
        FixedJoint joint;
        public void CalcuateDamage(float damage, GameObject Weapon)
        {
            if (damage > 10)
            {
                ZombieStateController.ChangeZombieState(ZombieStates.ZombieDead);

                attachedWeapon = Weapon;
                joint = attachedWeapon.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = this.GetComponent<Rigidbody>();

                joint.enableCollision = false; //collision between two game objects.
                joint.breakForce = 1000f;
                joint.breakTorque = 1000f;
            }
        }

        private void OnJointBreak(float breakForce)
        {
            joint.connectedBody = null;
        }
    }
}
