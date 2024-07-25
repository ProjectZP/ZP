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
        [SerializeField] private GameObject LeftHand;

        private IWeapon _currentWeaponRH;
        [SerializeField] private XRRayInteractor _rayInteractorRH;
        [SerializeField] private GameObject RightHand;

        private bool _isEquipTwoHandWeapon = false;

        private void Awake()
        {
            // Attach Event
            _rayInteractorLH.selectEntered.AddListener(args =>
            {
                var IWeaponComponent = args.interactableObject.transform.GetComponent<IWeapon>();
                if (IWeaponComponent == null)
                    return;

                IWeaponComponent.Equip();
                if (_isEquipTwoHandWeapon == true)
                {
                    CurrentWeaponLH = IWeaponComponent;

                    return;
                }

                _isEquipTwoHandWeapon = !IWeaponComponent.IsOneHanded();
                CurrentWeaponLH = IWeaponComponent;
                args.interactableObject.transform.SetParent(LeftHand.transform, false);
            });

            _rayInteractorRH.selectEntered.AddListener(args =>
            {
                var IWeaponComponent = args.interactableObject.transform.GetComponent<IWeapon>();
                if (IWeaponComponent == null)
                    return;

                IWeaponComponent.Equip();
                if (_isEquipTwoHandWeapon == true)
                {
                    CurrentWeaponRH = IWeaponComponent;

                    return;
                }

                _isEquipTwoHandWeapon = !IWeaponComponent.IsOneHanded();
                CurrentWeaponRH = IWeaponComponent;
                args.interactableObject.transform.SetParent(RightHand.transform, false);
            });

            // Deattach Event
            _rayInteractorLH.selectExited.AddListener(args =>
            {
                var IWeaponComponent = args.interactableObject.transform.GetComponent<IWeapon>();
                if (IWeaponComponent == null)
                    return;

                IWeaponComponent.DeEquip();
                if ((_isEquipTwoHandWeapon == true) && (CurrentWeaponRH != null))
                    args.interactableObject.transform.SetParent(RightHand.transform, false);
                else
                {
                    args.interactableObject.transform.SetParent(null);
                    _isEquipTwoHandWeapon = false;
                }
                CurrentWeaponLH = null;
            });
            _rayInteractorRH.selectExited.AddListener(args =>
            {
                var IWeaponComponent = args.interactableObject.transform.GetComponent<IWeapon>();
                if (IWeaponComponent == null)
                    return;

                IWeaponComponent.DeEquip();
                if ((_isEquipTwoHandWeapon == true) && (CurrentWeaponLH != null))
                    args.interactableObject.transform.SetParent(LeftHand.transform, false);
                else
                {
                    args.interactableObject.transform.SetParent(null);
                    _isEquipTwoHandWeapon = false;
                }
                CurrentWeaponRH = null;
            });
        }
    }
}