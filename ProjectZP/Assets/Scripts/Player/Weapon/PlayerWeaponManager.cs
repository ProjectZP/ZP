using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

namespace ZP.SJH.Weapon
{
    public class PlayerWeaponManager : MonoBehaviour
    {
        private const float DEFAULT_RAYCAST_DISTANCE = 2.5f;

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
        [SerializeField] private Transform _handModelLH;
        [SerializeField] private Transform _attachLH;

        private IWeapon _currentWeaponRH;
        [SerializeField] private XRRayInteractor _rayInteractorRH;
        [SerializeField] private Transform _handModelRH;
        [SerializeField] private Transform _attachRH;

        [SerializeField] private bool _isEquipTwoHandWeapon = false;
        [SerializeField] private bool _isEquippedInLeftHand; // Check for first grip of TwoHandWeapon

        private readonly Vector3 DEFAULT_ROTATION_RH = new Vector3(0, 0, -90);
        private readonly Vector3 DEFAULT_ROTATION_LH = new Vector3(0, 0, 90);
        private void Awake()
        {
            // Attach Event
            _rayInteractorLH.selectEntered.AddListener(args =>
            {
                var IWeaponComponent = args.interactableObject.transform.GetComponent<IWeapon>();
                if (IWeaponComponent == null)
                    return;

                // If already equip TwoHandWeapon in right hand
                if (_isEquipTwoHandWeapon == true)
                {
                    _isEquippedInLeftHand = false;
                    CurrentWeaponLH = IWeaponComponent;

                    Transform weaponTransform = args.interactableObject.transform;

                    Transform handTransform
                = (weaponTransform.Find("AttachPointSecondary") != null)
                ? weaponTransform.Find("AttachPointSecondary").transform
                : weaponTransform.transform;

                    _handModelLH.parent = handTransform;
                    _handModelLH.localPosition = Vector3.zero;
                    _rayInteractorLH.maxRaycastDistance = 0f;
                    IWeaponComponent.Equip(false);

                    return;
                }

                // Equip weapon
                EquipWeapon(_attachLH, args.interactableObject.transform, IWeaponComponent, false);

                _isEquipTwoHandWeapon = !IWeaponComponent.IsOneHanded();
                if (IWeaponComponent.IsOneHanded() == false)
                    _isEquippedInLeftHand = true;

                CurrentWeaponLH = IWeaponComponent;
            });

            _rayInteractorRH.selectEntered.AddListener(args =>
            {
                var IWeaponComponent = args.interactableObject.transform.GetComponent<IWeapon>();
                if (IWeaponComponent == null)
                    return;

                // If already equip TwoHandWeapon in left hand
                if (_isEquipTwoHandWeapon == true)
                {
                    _isEquippedInLeftHand = true;
                    CurrentWeaponRH = IWeaponComponent;

                    Transform weaponTransform = args.interactableObject.transform;

                    Transform handTransform
                = (weaponTransform.Find("AttachPointSecondary") != null)
                ? weaponTransform.Find("AttachPointSecondary").transform
                : weaponTransform.transform;

                    _handModelRH.parent = handTransform;
                    _handModelRH.localPosition = Vector3.zero;
                    _rayInteractorRH.maxRaycastDistance = 0f;
                    IWeaponComponent.Equip(false);

                    return;
                }

                // Equip weapon
                EquipWeapon(_attachRH, args.interactableObject.transform, IWeaponComponent, false);

                _isEquipTwoHandWeapon = !IWeaponComponent.IsOneHanded();
                if (IWeaponComponent.IsOneHanded() == false)
                    _isEquippedInLeftHand = false;

                CurrentWeaponRH = IWeaponComponent;
            });

            // Deattach Event
            _rayInteractorLH.selectExited.AddListener(args =>
            {
                var IWeaponComponent = args.interactableObject.transform.GetComponent<IWeapon>();
                if (IWeaponComponent == null)
                    return;

                ResetHandModel(true);

                IWeaponComponent.DeEquip();
                // Case LH Enter -> RH Enter -> LH Exit : Move to Right
                if ((_isEquipTwoHandWeapon == true) && (CurrentWeaponRH != null) && (_isEquippedInLeftHand == true))
                {
                    ResetHandModel(false);
                    EquipWeapon(_attachRH, args.interactableObject.transform, IWeaponComponent, true);
                }
                // Case RH Enter -> LH Enter -> LH Exit : Stay in Right
                else if ((_isEquipTwoHandWeapon == true) && (CurrentWeaponRH != null) && (_isEquippedInLeftHand == false))
                { }
                // Case LH Enter -> LH Exit : Drop
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

                ResetHandModel(false);

                IWeaponComponent.DeEquip();
                // Case RH Enter -> LH Enter -> RH Exit : Move to Left
                if ((_isEquipTwoHandWeapon == true) && (CurrentWeaponLH != null) && (_isEquippedInLeftHand == false))
                {
                    ResetHandModel(true);
                    EquipWeapon(_attachLH, args.interactableObject.transform, IWeaponComponent, true);
                }
                // Case LH Enter -> RH Enter -> RH Exit : Stay in Left
                else if ((_isEquipTwoHandWeapon == true) && (CurrentWeaponLH != null) && (_isEquippedInLeftHand == true))
                {
                }
                // Case RH Enter -> RH Exit : Drop
                else
                {
                    args.interactableObject.transform.SetParent(null);
                    _isEquipTwoHandWeapon = false;
                }

                CurrentWeaponRH = null;
            });
        }


        private void EquipWeapon(Transform to, Transform weapon, IWeapon IWeaponComponent, bool isMoving)
        {
            IWeaponComponent.SetConstraint(false);

            Transform weaponTransform
                = (weapon.Find("AttachPointGrip") != null)
                ? weapon.Find("AttachPointGrip").transform
                : weapon.transform;

            weapon.parent = to;

            weapon.localPosition = weaponTransform.localPosition;
            weapon.localRotation = weaponTransform.localRotation;

            IWeaponComponent.Equip(isMoving);
        }

        private void ResetHandModel(bool isLeft)
        {
            if (isLeft == true)
            {
                _handModelLH.parent = _rayInteractorLH.transform;
                _handModelLH.localPosition = Vector3.zero;
                _handModelLH.localRotation = Quaternion.Euler(DEFAULT_ROTATION_LH);
                _rayInteractorLH.maxRaycastDistance = DEFAULT_RAYCAST_DISTANCE;
            }
            else
            {
                _handModelRH.parent = _rayInteractorRH.transform;
                _handModelRH.localPosition = Vector3.zero;
                _handModelRH.localRotation = Quaternion.Euler(DEFAULT_ROTATION_RH);
                _rayInteractorRH.maxRaycastDistance = DEFAULT_RAYCAST_DISTANCE;

            }
        }
        private IEnumerator C_MoveWeapon(Transform to, Transform weapon)
        {
            const float MOVE_SPEED = 1f;

            Transform weaponTransform
                = (weapon.Find("AttachPointGrip") != null)
                ? weapon.Find("AttachPointGrip").transform
                : weapon.transform;

            while ((to.position - weaponTransform.position).sqrMagnitude > 0.01f
                || Quaternion.Angle(to.rotation, weaponTransform.rotation) > 0.01f)
            {
                weaponTransform.position = Vector3.Lerp(weaponTransform.position, to.position, MOVE_SPEED);
                weaponTransform.rotation = Quaternion.Lerp(weaponTransform.rotation, to.rotation, MOVE_SPEED);

                yield return null;
            }
        }
    }
}
