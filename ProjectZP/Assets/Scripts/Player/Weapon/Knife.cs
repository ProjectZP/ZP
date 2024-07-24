using UnityEngine;
using ZP.BHS.Zombie;

namespace ZP.SJH.Weapon
{
    public class Knife : BaseWeapon, IWeapon
    {
        private Vector3 _positionBuffer;
        private float _velocity;

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
            _positionBuffer = transform.position;
        }

        private void Update()
        {
            _positionBuffer = transform.position;
            _velocity = ((_positionBuffer - transform.position) / Time.deltaTime).magnitude;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == ZombieLayer && _velocity >= _weaponData.MinVelocity) 
                collision.gameObject.GetComponent<ZombieDamageController>()
                    .OnGetDamaged.Invoke(CalculateDamage());
        }

        public float CalculateDamage()
        {
            float damage = _weaponData.Sharpness * _velocity;
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