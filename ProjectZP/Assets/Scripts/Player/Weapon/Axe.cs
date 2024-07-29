using UnityEngine;
using ZP.BHS.Zombie;

namespace ZP.SJH.Weapon
{
    public class Axe : BaseWeapon, IWeapon
    {
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
            if (other.gameObject.layer == ZombieLayer 
                && _rigidbody.velocity.magnitude >= _weaponData.MinVelocity 
                && other.gameObject.GetComponent<ZombieDamageController>())
            {
                other.gameObject.GetComponent<ZombieDamageController>()
                    .OnGetDamaged?.Invoke(CalculateDamage(), this.gameObject);
            }
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
            => _rigidbody.constraints = isFreeze ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;

        public int GetHandCount()
        {
            return _handCount;
        }
    }
}