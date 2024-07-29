using UnityEngine;
using ZP.BHS.Zombie;

namespace ZP.SJH.Weapon
{
    public class Knife : BaseWeapon, IWeapon
    {
        public override WeaponData WeaponData
        {
            get => _weaponData;
            set => _weaponData = value;
        }

        protected override void Awake()
        {
            base.Awake();

            if (_weaponData == null)
                _weaponData = Resources.Load("Data/KnifeData") as WeaponData;
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

        public float CalculateDamage()
        {
            float damage = _weaponData.Sharpness * _rigidbody.velocity.magnitude;

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

        public void Equip(bool isMoving)
        {
            return;
        }

        public void DeEquip()
        {
            return;
        }

        public int GetHandCount()
        {
            return -1;
        }

        public void SetConstraint(bool isFreeze)
            => _rigidbody.constraints = isFreeze ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
    }
}