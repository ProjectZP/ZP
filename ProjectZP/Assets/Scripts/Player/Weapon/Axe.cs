using UnityEngine;
using ZP.BHS.Zombie;

namespace ZP.SJH.Weapon
{
    public class Axe : BaseWeapon, IWeapon
    {
        [SerializeField] BoxCollider _trigger;
        private Vector3 _positionBuffer;
        private float _velocity;
        private float _elapsedTime = 0f;

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
            _positionBuffer = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger Enter");
            Debug.Log(other.gameObject.layer);
            Debug.Log(other.gameObject.GetComponent<ZombieCore>());

            var damage = CalculateDamage();
            Debug.Log(damage);

            if (other.gameObject.layer == ZombieLayer
                && other.gameObject.GetComponent<ZombieCore>()
                && damage > 60f)
            {
                Debug.Log("Zombie!");
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
            _elapsedTime += Time.deltaTime;
            if(_elapsedTime > 0.1f)
            {
                _velocity = ((transform.position - _positionBuffer) / 0.1f).magnitude;
                //_velocity -= abs(triggerValue) * moveSpeed;
                _positionBuffer = transform.position;
                _elapsedTime = 0f;
            }
            Debug.LogWarning(_velocity);
            CalculateDamage();

            
        }

        public float CalculateDamage()
        {
            float damage = _weaponData.Sharpness * _velocity;

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