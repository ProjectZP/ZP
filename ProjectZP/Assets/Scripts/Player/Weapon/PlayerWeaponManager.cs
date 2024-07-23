using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
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

        private void Awake()
        {
            // Attach Event
            _rayInteractorLH.selectEntered.AddListener(args =>
            {
                var IWeaponComponent = args.interactableObject.transform.GetComponent<IWeapon>();
                if (IWeaponComponent != null)
                    CurrentWeaponLH = IWeaponComponent;
            });
            _rayInteractorRH.selectEntered.AddListener(args =>
            {
                var IWeaponComponent = args.interactableObject.transform.GetComponent<IWeapon>();
                if (IWeaponComponent != null)
                    CurrentWeaponRH = IWeaponComponent;
            });

            // Deattach Event
            _rayInteractorLH.selectExited.AddListener(args =>
            {
                CurrentWeaponLH = null;
            });
            _rayInteractorRH.selectExited.AddListener(args =>
            {
                CurrentWeaponRH = null;
            });
        }
    }
}