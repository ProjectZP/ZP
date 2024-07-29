using System;
using UnityEngine;

namespace ZP.Villin.World
{
    /// <summary>
    /// Build a world with logic when game starts.
    /// </summary>
    public class DynamicWorldConstructor : MonoBehaviour
    {
        public Action<GameObject> OnDoorsAndWindowsGenerated;
        [SerializeField] private Transform _elevatingTransform;
        [SerializeField] private Transform _rotatableWorldTransform;
        [SerializeField] private GameObject _groundFloorElement;
        [SerializeField] private GameObject _elevatingChildElement;
        [SerializeField] private GameObject _doorsAndWindowsElement;
        [SerializeField] private GameObject _roofElement;
        [SerializeField] private int _totalFloorCount;
        [SerializeField] private int _gameStartFloorCount;
        [SerializeField] private float _floorGap;
        [SerializeField] private float _floorGapOffset;
        private int _teleportableCount;

        private void Awake()
        {
            CheckAwakeException();
            MakeFloors();
            GetLightObject();
        }

        /// <summary>
        /// getter of <see cref="_floorGap"/>
        /// </summary>
        /// <returns><see cref="_floorGap"/></returns>
        public float GetFloorGap()
        {
            return _floorGap;
        }


        /// <summary>
        /// getter of <see cref="_teleportableCount"/>
        /// </summary>
        /// <returns><see cref="_teleportableCount"/></returns>
        public int GetTeleportableCount()
        {
            return _teleportableCount;
        }

        public int GetTotalFloorCount()
        {
            return _totalFloorCount;
        }

        /// <summary>
        /// Check Exception When Class Awake.
        /// </summary>
        private void CheckAwakeException()
        {
            if (_rotatableWorldTransform == default)
            {
#if UNITY_EDITOR
                Debug.Log("_rotatableField in Teleport Manager is null!");
#endif
            }

            if (_elevatingChildElement == default)
            {
#if UNITY_EDITOR
                Debug.Log("_movableField in Teleport Manager is null!");
#endif
            }

            if (_doorsAndWindowsElement == default)
            {
#if UNITY_EDITOR
                Debug.Log("_doorsAndWindowsElement in Teleport Manager is null!");
#endif
            }

            if (_roofElement == default)
            {
#if UNITY_EDITOR
                Debug.Log("_roofElement in Teleport Manager is null!");
#endif
            }
        }

        /// <summary>
        /// Build a world with various SerializedFields in Unity inspector.
        /// </summary>
        private void MakeFloors()
        {
            // set postion to Vector3.zero to make floors.
            transform.root.position = Vector3.zero;
            _elevatingTransform.position = Vector3.zero;
            _teleportableCount = _totalFloorCount - _gameStartFloorCount;
            Vector3 refPosition = new Vector3(_elevatingChildElement.transform.position.x, 0f, _elevatingChildElement.transform.position.z);
            Quaternion refRotation = _elevatingChildElement.transform.rotation;
            float nextY = -((_gameStartFloorCount - 1) * _floorGap) + _floorGapOffset;
            _rotatableWorldTransform.position = new Vector3(0f, nextY, 0f);
            _groundFloorElement.transform.position = _rotatableWorldTransform.position + new Vector3(refPosition.x, 0f, refPosition.z);
            for (int x = 2; x <= _totalFloorCount; ++x)
            {
                nextY += _floorGap;
                Vector3 newPosition = new Vector3(refPosition.x, nextY, refPosition.z);
                GameObject spawnedFloor;
                GameObject spawnedDoorsAndWindows;
                if (x == _totalFloorCount)
                {
                    spawnedFloor = Instantiate(_roofElement, newPosition, refRotation, _elevatingTransform);
                }
                else
                {
                    spawnedFloor = Instantiate(_elevatingChildElement, newPosition, refRotation, _elevatingTransform);
                }
                spawnedFloor.name = $"floor {x} rail";
                spawnedDoorsAndWindows = Instantiate(_doorsAndWindowsElement, newPosition, refRotation, _elevatingTransform);
                spawnedDoorsAndWindows.name = $"floor {x} Doors and Windows";
                OnDoorsAndWindowsGenerated?.Invoke(spawnedDoorsAndWindows);
                if (x == _gameStartFloorCount)
                {
                    spawnedDoorsAndWindows.SetActive(false);
                }
            }
            Destroy(_elevatingChildElement);
            Destroy(_doorsAndWindowsElement);
            Destroy(_roofElement);
        }

        /// <summary>
        /// Get <see cref="Light"/> and Set as child of <see cref="_rotatableWorldTransform"/> for tricking light when world rotates.
        /// </summary>
        private void GetLightObject()
        {
            Light light = FindFirstObjectByType<Light>();
            Debug.Log($"Light transform : {light.transform}");
            light.transform.SetParent(_rotatableWorldTransform);
        }
    }
}
