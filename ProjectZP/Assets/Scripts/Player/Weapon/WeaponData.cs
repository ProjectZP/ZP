using UnityEngine;

namespace ZP.SJH.Weapon
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Create WeaponData")]
    public class WeaponData : ScriptableObject
    {
        public enum WeaponType
        {
            None,
            TwoHandedAxe,
            Knife
        }

        public WeaponType Type;
        public bool IsOneHanded;
        public float Sharpness;
        public float MinVelocity;
    }
}