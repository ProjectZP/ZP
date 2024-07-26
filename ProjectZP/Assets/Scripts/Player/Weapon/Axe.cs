using UnityEngine;
using ZP.BHS.Zombie;

namespace ZP.SJH.Weapon
{
    public class Axe : BaseWeapon, IWeapon
    {
        public BoxCollider trigboxcol; //BHS Added.

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

        //private void OnCollisionEnter(Collision collision) //BHS Added.
        //{
        //    if (collision.gameObject.layer == ZombieLayer && _rigidbody.velocity.magnitude >= _weaponData.MinVelocity && collision.gameObject.GetComponent<ZombieHeadDefense>())
        //    {
        //        collision.gameObject.GetComponent<ZombieHeadDefense>()
        //            .OnGetDamaged?.Invoke(CalculateDamage(),this.gameObject);
        //    }
        //}

        private void OnTriggerEnter(Collider other) //BHS Added.
        {
            Debug.Log("Axe Damage : " + CalculateDamage());
            if (CalculateDamage() > 1000f)
            {
                Debug.Log("Damage Over Defense");
                Debug.Log(other.name);
            }
            else
            {
                trigboxcol.isTrigger = false; //BHS Added.
                Debug.Log("Damage Under Defense.");
            }
            //if (other.gameObject.layer == ZombieLayer && other.gameObject.GetComponent<ZombieCore>()) //other.gameObject.GetComponent<ZombieHeadDefense>() _rigidbody.velocity.magnitude >= _weaponData.MinVelocity && 
            //{
            //    other.gameObject.GetComponent<ZombieCore>()
            //        .OnGetDamaged?.Invoke(CalculateDamage(), this.gameObject);
            //}
        }

        private void OnTriggerExit(Collider other) //BHS Added.
        {
            trigboxcol.isTrigger = true;
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

            //if (damage > 1f) // BHS Did.
            //    Debug.Log("DAMANNNNAAAAAAA" + damage);

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