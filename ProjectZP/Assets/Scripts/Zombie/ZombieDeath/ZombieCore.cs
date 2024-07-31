using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

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

        private void CalcuateDamage(float damage, GameObject Weapon)
        {
            if (damage > 60f)
            {
                zombieStateController.ChangeZombieState(ZombieStates.ZombieDead);

                attachedWeapon = Weapon.transform.gameObject;
                joint = attachedWeapon.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = this.GetComponent<Rigidbody>();

                joint.enableCollision = false;
                joint.breakForce = 500f;
                joint.breakTorque = 500f;

                zombieManager.HeadIK.transform.GetComponentInChildren<TwoBoneIKConstraint>().weight = 0;

                Instantiate(BloodEffect, transform.position, Weapon.transform.rotation);
                
                Destroy(NeckSpring);
                Destroy(neck.GetComponent<CharacterJoint>());
                Destroy(neck.GetComponent<Rigidbody>());

                Head.GetComponent<CharacterJoint>().connectedBody = Spine.GetComponent<Rigidbody>();
            }
        }

        private void OnJointBreak(float breakForce)
        {
            joint.connectedBody = null;
        }
    }
}
