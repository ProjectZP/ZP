using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;
using UnityEngine.InputSystem.EnhancedTouch;
using ZP.SJH.Weapon;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This Class Manages Zombie's Death.
    /// Zombie Have Core and when Weapon Hit it, Zombie Die.
    /// </summary>
    class ZombieCore : MonoBehaviour
    {
        public UnityEvent<float, GameObject> OnGetDamaged;

        GameObject attachedWeapon;
        FixedJoint joint;
        ZombieManager zombieManager;
        ZombieStateController zombieStateController;
        [SerializeField] SpringJoint NeckSpring;
        [SerializeField] GameObject Spine;
        [SerializeField] GameObject Head;
        [SerializeField] GameObject BloodEffect;
        GameObject neck;

        private void Awake()
        {
            zombieManager = GetComponentInParent<ZombieManager>();
            zombieStateController = GetComponentInParent<ZombieStateController>();
            neck = NeckSpring.gameObject;
        }

        private void Start()
        {
            OnGetDamaged?.AddListener(CalcuateDamage);
        }

        public void CalcuateDamage(float damage, GameObject Weapon) //Todo: Core Script
        {
            if (damage > 60f)
            {
                Debug.Log("Damage Over Defense");

                //============================
                //Debug.Log("Core Damaged");

                zombieStateController.ChangeZombieState(ZombieStates.ZombieDead);

                attachedWeapon = Weapon.transform.gameObject;
                joint = attachedWeapon.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = this.GetComponent<Rigidbody>();

                joint.enableCollision = false; //collision between two game objects.
                joint.breakForce = 500f;
                joint.breakTorque = 500f;

                zombieManager.HeadIK.transform.GetComponentInChildren<TwoBoneIKConstraint>().weight = 0;

                Instantiate(BloodEffect, transform.position, Weapon.transform.rotation);
                
                Destroy(NeckSpring);
                Destroy(neck.GetComponent<CharacterJoint>());
                Destroy(neck.GetComponent<Rigidbody>());
                

                Head.GetComponent<CharacterJoint>().connectedBody = Spine.GetComponent<Rigidbody>();
                //============================

                //zombieStateController.ChangeZombieState(ZombieStates.ZombieDead);
            }
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.layer == LayerMask.NameToLayer("Weapon") && zombieStateController.currentZombieState != ZombieStates.ZombieDead)
        //    {
        //        Debug.Log("Core Damaged");

        //        zombieStateController.ChangeZombieState(ZombieStates.ZombieDead);

        //        attachedWeapon = other.transform.gameObject;
        //        joint = attachedWeapon.gameObject.AddComponent<FixedJoint>();
        //        joint.connectedBody = this.GetComponent<Rigidbody>();

        //        joint.enableCollision = false; //collision between two game objects.
        //        joint.breakForce = 500f;
        //        joint.breakTorque = 500f;

        //        zombieManager.HeadIK.transform.GetComponentInChildren<TwoBoneIKConstraint>().weight = 0;

        //        Destroy(NeckSpring);
        //        Destroy(NeckSpring.GetComponent<CharacterJoint>());
        //        Destroy(NeckSpring.GetComponent<Rigidbody>());

        //        Head.GetComponent<CharacterJoint>().connectedBody = Spine.GetComponent<Rigidbody>();
        //    }
        //}
        private void OnCollisionEnter(Collision collision)
        {

        }

        private void OnJointBreak(float breakForce) //Todo: Core script
        {
            joint.connectedBody = null;
        }
    }
}
