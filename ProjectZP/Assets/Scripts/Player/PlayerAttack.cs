using UnityEngine;
using System.Collections;
using ZP.SJH.Weapon;

namespace ZP.SJH.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private const float MIN_ATTACK_TIME = 0.2f;
        private const float ATTACK_STAMINA = 20f;

        [SerializeField] private PlayerWeaponManager _weaponManager;
        [SerializeField] private PlayerStatusManager _statusManager;

        [SerializeField] private GameObject _controllerLH;
        [SerializeField] private Rigidbody _rigidbodyLH;
        private Vector3 _prevPositionLH = Vector3.zero;

        [SerializeField] private GameObject _controllerRH;
        [SerializeField] private Rigidbody _rigidbodyRH;
        private Vector3 _prevPositionRH = Vector3.zero;

        private void Awake()
        {
            if (_controllerLH == null)
                _controllerLH = GameObject.Find("Left Controller");
            if (_rigidbodyLH == null)
                _rigidbodyLH = _controllerLH.GetComponent<Rigidbody>();
            if (_controllerRH == null)
                _controllerRH = GameObject.Find("Right Controller");
            if (_rigidbodyRH == null)
                _rigidbodyRH = _controllerRH.GetComponent<Rigidbody>();
        }

        bool isAttacking;
        float time = 0f;
        private void Update()
        {
            if ((_statusManager.CurrentStamina >= ATTACK_STAMINA) && _weaponManager.CurrentWeaponLH != null)
                CheckLeftHandAttack();
            if ((_statusManager.CurrentStamina >= ATTACK_STAMINA) && _weaponManager.CurrentWeaponRH != null)
                CheckRightHandAttack();

        }

        private void CheckRightHandAttack()
        {
            // Check minimum velocity
            if (_rigidbodyRH.velocity.magnitude >= _weaponManager.CurrentWeaponRH.GetMinVelocity())
            {
                time += Time.deltaTime;
                if (isAttacking == true)
                {
                    if (time >= MIN_ATTACK_TIME)
                    {
                        _statusManager.CurrentStamina -= ATTACK_STAMINA;
                        if (_statusManager.CurrentStamina < 0.1f)
                            _statusManager.CurrentStamina = 0.1f;

                            time = 0f;
                    }
                }
                else
                {
                    isAttacking = true;
                }
            }
            else
            {
                isAttacking = false;
                time = 0f;
            }
        }

        private void CheckLeftHandAttack()
        {
            // Check minimum velocity
            if (_rigidbodyLH.velocity.magnitude >= _weaponManager.CurrentWeaponLH.GetMinVelocity())
            {
                time += Time.deltaTime;
                if (isAttacking == true)
                {
                    if (time >= MIN_ATTACK_TIME)
                    {
                        _statusManager.CurrentStamina -= ATTACK_STAMINA;
                        if (_statusManager.CurrentStamina < 0.1f)
                            _statusManager.CurrentStamina = 0.1f;
                        time = 0f;
                    }
                }
                else
                {
                    isAttacking = true;
                }
            }
            else
            {
                isAttacking = false;
                time = 0f;
            }
        }
    }
}