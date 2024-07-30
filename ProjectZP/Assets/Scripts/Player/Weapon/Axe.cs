using UnityEngine;
using ZP.BHS.Zombie;
using ZP.SJH.Player;

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
            _positionBuffer = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            var damage = CalculateDamage();
            Debug.LogWarning($"Axe {damage}");
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
            _elapsedTime += Time.deltaTime;
            if(_elapsedTime > 0.1f)
            {
                _velocity = ((transform.position - _positionBuffer) / 0.1f).magnitude;

                if (_handCount > 0 && _playerManager != null)
                    _velocity -= _playerManager.Input.MoveValue * _playerManager.Status.MoveSpeed;

                _velocity = Mathf.Abs(_velocity);

                _positionBuffer = transform.position;
                _elapsedTime = 0f;
            }
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
            if(_playerManager == null)
                _playerManager = FindAnyObjectByType<PlayerManager>();
            if(isMoving == false)
                _handCount++;
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        public void DeEquip()
        {
            if (--_handCount <= 0)
                _rigidbody.constraints = RigidbodyConstraints.None;
        }

        public int GetHandCount()
        {
            return _handCount;
        }

        public void SetConstraint(bool isFreeze) 
            => _rigidbody.constraints = isFreeze 
            ? RigidbodyConstraints.FreezeAll 
            : RigidbodyConstraints.None;
    }
}