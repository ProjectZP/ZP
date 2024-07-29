using UnityEngine;
using ZP.BHS.Zombie;

namespace ZP.SJH.Weapon
{
    public class Axe : BaseWeapon, IWeapon
    {
        [SerializeField] BoxCollider _trigger;

        public override WeaponData WeaponData
        {
            get => _weaponData;
            set => _weaponData = value;
        }

        [SerializeField] private int _handCount = 0;

        protected override void Awake()
        {
            base.Awake();

            if (_weaponData == null)
                _weaponData = Resources.Load("Data/AxeData") as WeaponData;
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var damage = CalculateDamage();
            if (other.gameObject.layer == ZombieLayer
                && other.gameObject.GetComponent<ZombieCore>()
                && damage > 60f)
            {
                other.gameObject.GetComponent<ZombieCore>()
                    .OnGetDamaged?.Invoke(damage, this.gameObject);
            }
            else
                _trigger.isTrigger = false;
        }

        private void OnTriggerExit(Collider other)
        {
            _trigger.isTrigger = true;
        }

        private void Update()
        {
            CalculateDamage();
        }

        public float CalculateDamage()
        {
            float damage = _weaponData.Sharpness * _rigidbody.velocity.magnitude;

            if (_handCount < 2)
                damage /= 3f;

            return damage;
        }

        public float GetMinVelocity()
        {
            return WeaponData.MinVelocity;
        }

        public bool IsOneHanded()
        {
            return WeaponData.IsOneHanded;
        }

        /// <summary>
        /// Set its rigidbody constraints freezed
        /// If it's moving to other hand, no need to increase hand count
        /// </summary>
        /// <param name="isMoving"></param>
        public void Equip(bool isMoving)
        {
            if(isMoving == false)
                _handCount++;
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        public void DeEquip()
        {
            if (--_handCount <= 0)
                _rigidbody.constraints = RigidbodyConstraints.None;
        }

        public void SetConstraint(bool isFreeze) 
            => _rigidbody.constraints = isFreeze 
            ? RigidbodyConstraints.FreezeAll 
            : RigidbodyConstraints.None;

        public int GetHandCount()
        {
            return _handCount;
        }
    }
}