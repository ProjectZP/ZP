using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ZP.SJH.Player;

namespace ZP.SJH.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseWeapon : MonoBehaviour
    {
        protected readonly string ZombieLayerName = "Zombie";
        protected int ZombieLayer { get; private set; }

        protected Vector3 _positionBuffer;
        protected float _velocity;
        protected float _elapsedTime = 0f;
        protected PlayerManager _playerManager;

        abstract public WeaponData WeaponData { get; set; }
        [SerializeField] protected WeaponData _weaponData;

        [SerializeField] protected Rigidbody _rigidbody;

        protected virtual void Awake()
        {
            if(_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();

            ZombieLayer = LayerMask.NameToLayer(ZombieLayerName);
        }
    }
}