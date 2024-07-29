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

        private void Update()
        {

            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > 0.1f)
            {
                _velocity = ((transform.position - _positionBuffer) / 0.1f).magnitude;

                if (_isHold && _playerManager != null)
                    _velocity -= Mathf.Abs(_playerManager.MoveValue) * _playerManager.MoveSpeed;
                _positionBuffer = transform.position;
                _elapsedTime = 0f;
            }
            CalculateDamage();
        }

        private void OnTriggerExit(Collider other)
        {
            _trigger.isTrigger = true;
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
            _isHold = true;
            return;
        }

        public void DeEquip()
        {
            _isHold = false;
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