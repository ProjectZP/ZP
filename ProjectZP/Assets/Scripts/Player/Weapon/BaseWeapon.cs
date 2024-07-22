using UnityEngine;

namespace ZP.SJH.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseWeapon : MonoBehaviour
    {
        protected readonly string ZombieLayerName = "Zombie";
        protected int ZombieLayer { get; private set; }

        abstract public WeaponData WeaponData { get; set; }
        [SerializeField] protected WeaponData _weaponData;

        protected Rigidbody _rigidbody;

        protected virtual void Awake()
        {
            if(_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();

            ZombieLayer = LayerMask.NameToLayer(ZombieLayerName);
        }
    }
}