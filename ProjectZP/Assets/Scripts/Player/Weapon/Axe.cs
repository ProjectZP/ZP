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

        private int _handCount = 0;

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
            if (collision.gameObject.layer == ZombieLayer && _rigidbody.velocity.magnitude >= _weaponData.MinVelocity && collision.gameObject.GetComponent<ZombieDamageController>())
            {
                collision.gameObject.GetComponent<ZombieDamageController>()
                    .OnGetDamaged?.Invoke(CalculateDamage(),this.gameObject);
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

        public void Equip()
        {
            _handCount++;
        }

        public void DeEquip()
        {
            _handCount--;
        }
    }
}