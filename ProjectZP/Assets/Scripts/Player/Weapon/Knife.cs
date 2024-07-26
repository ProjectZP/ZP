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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == ZombieLayer && _rigidbody.velocity.magnitude >= _weaponData.MinVelocity && collision.gameObject.GetComponent<ZombieHeadDefense>())
            {
                //collision.gameObject.GetComponent<ZombieHeadDefense>() //BHS Did.
                //    .OnGetDamaged?.Invoke(CalculateDamage(), this.gameObject);
            }
        }

        public float CalculateDamage()
        {
            float damage = _weaponData.Sharpness * _rigidbody.velocity.magnitude;
            if(damage > 1f)
                Debug.Log("DAMANNNN" + damage);
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
            return;
        }

        public void DeEquip()
        {
            return;
        }
    }
}