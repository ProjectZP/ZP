using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ZP.SJH.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseWeapon : MonoBehaviour
    {
        protected readonly string ZombieLayerName = "Zombie";
        protected int ZombieLayer { get; private set; }

        abstract public WeaponData WeaponData { get; set; }
        [SerializeField] protected WeaponData _weaponData;

        [SerializeField] protected Rigidbody _rigidbody;
        protected bool _isHolded = false;

        protected virtual void Awake()
        {
            if(_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();

            ZombieLayer = LayerMask.NameToLayer(ZombieLayerName);
        }
    }
}