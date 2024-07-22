using System.Collections.Generic;
using UnityEngine;
using WeaponType = ZP.SJH.Weapon.WeaponData.WeaponType;

namespace ZP.SJH.Weapon
{
    public class PlayerWeaponManager : MonoBehaviour
    {
        public enum WeaponGripType
        {
            LeftHand,
            RightHand, 
            TwoHand
        }

        public IWeapon CurrentWeaponLH
        {
            get => _currentWeaponLH;
            private set => _currentWeaponLH = value;
        }
        public IWeapon CurrentWeaponRH
        {
            get => _currentWeaponRH;
            private set => _currentWeaponRH = value;
        }

        private IWeapon _currentWeaponLH;
        private IWeapon _currentWeaponRH;

        void ArmWeapon(WeaponType type, WeaponGripType gripType)
        {

        }

        void DisarmWeapon(WeaponGripType gripType)
        {

        }
    }

}