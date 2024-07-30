using UnityEngine;
using ZP.BHS.Zombie;
using ZP.SJH.Player;

namespace ZP.SJH.Weapon
{
    public class Knife : BaseWeapon, IWeapon
    {
        [SerializeField] private  CapsuleCollider _trigger;

        public override WeaponData WeaponData
        {
            get => _weaponData;
            set => _weaponData = value;
        }

        private bool _isHold = false;

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
            var damage = CalculateDamage();
            Debug.LogWarning($"Knife {damage}");
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
            if (_elapsedTime > 0.1f)
            {
                _velocity = ((transform.position - _positionBuffer) / 0.1f).magnitude;

                if (_isHold && _playerManager != null)
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
            if (_playerManager == null)
                _playerManager = FindAnyObjectByType<PlayerManager>();
            _isHold = true;
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            return;
        }

        public void DeEquip()
        {
            _isHold = false;
            _rigidbody.constraints = RigidbodyConstraints.None;
            return;
        }

        public int GetHandCount()
        {
            return -1;
        }

        public void SetConstraint(bool isFreeze)
            => _rigidbody.constraints = isFreeze 
            ? RigidbodyConstraints.FreezeAll 
            : RigidbodyConstraints.None;
    }
}