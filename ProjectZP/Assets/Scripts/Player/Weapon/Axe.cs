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

        protected override void Awake()
        {
            base.Awake();

            if (_weaponData == null)
                _weaponData = Resources.Load("Data/AxeData") as WeaponData;
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == ZombieLayer && _rigidbody.velocity.magnitude >= _weaponData.MinVelocity)
            {
                collision.gameObject.GetComponent<ZombieDamageController>()
                    .OnGetDamaged?.Invoke(CalculateDamage());
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
    }

}