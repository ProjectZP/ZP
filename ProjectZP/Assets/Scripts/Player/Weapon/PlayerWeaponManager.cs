using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using WeaponType = ZP.SJH.Weapon.WeaponData.WeaponType;

namespace ZP.SJH.Weapon
{
    public class PlayerWeaponManager : MonoBehaviour
    {
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
        
        /*
        public IWeapon CurrentWeaponLH
        {
            get { return _currentWeaponLH; }
            private set { _currentWeaponLH = value; Debug.Log("LH : " + _currentWeaponLH); }
        }
        public IWeapon CurrentWeaponRH
        {
            get { return _currentWeaponRH; }
            private set { _currentWeaponRH = value; Debug.Log("RH : " + _currentWeaponRH); }
        }
        */

        private IWeapon _currentWeaponLH;
        [SerializeField] private XRRayInteractor _rayInteractorLH;

        private IWeapon _currentWeaponRH;
        [SerializeField] private XRRayInteractor _rayInteractorRH;

        private bool _isEquipTwoHandWeapon = false;
        private void Awake()
        {
            // Attach Event
            _rayInteractorLH.selectEntered.AddListener(args =>
            {
                var IWeaponComponent = args.interactableObject.transform.GetComponent<IWeapon>();
                if (IWeaponComponent != null)
                {
                    if (_isEquipTwoHandWeapon == true)
                        return;

                    // Cannot equip two handed weapon while the other hand is not empty
                    if (IWeaponComponent.IsOneHanded() == false && CurrentWeaponRH != null) 
                        return;

                    _isEquipTwoHandWeapon = !IWeaponComponent.IsOneHanded();
                    CurrentWeaponLH = IWeaponComponent;
                }
            });
            _rayInteractorRH.selectEntered.AddListener(args =>
            {
                var IWeaponComponent = args.interactableObject.transform.GetComponent<IWeapon>();
                if (IWeaponComponent != null)
                {
                    if (_isEquipTwoHandWeapon == true)
                        return;

                    // Cannot equip two handed weapon while the other hand is not empty
                    if (IWeaponComponent.IsOneHanded() == false && CurrentWeaponLH != null)
                        return;

                    _isEquipTwoHandWeapon = !IWeaponComponent.IsOneHanded();
                    CurrentWeaponRH = IWeaponComponent;
                }
            });

            // Deattach Event
            _rayInteractorLH.selectExited.AddListener(args =>
            {
                CurrentWeaponLH = null;
                _isEquipTwoHandWeapon = false;
            });
            _rayInteractorRH.selectExited.AddListener(args =>
            {
                CurrentWeaponRH = null;
                _isEquipTwoHandWeapon = false;
            });
        }
    }
}