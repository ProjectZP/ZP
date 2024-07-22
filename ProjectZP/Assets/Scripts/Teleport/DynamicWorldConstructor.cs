using UnityEngine;

namespace ZP.Villin.World
{
    /// <summary>
    /// Build a world with logic when game starts.
    /// </summary>
    public class DynamicWorldConstructor : MonoBehaviour
    {
        [SerializeField] private Transform _elevatingTransform;
        [SerializeField] private Transform _rotatableWorldTransform;
        [SerializeField] private GameObject _groundFloorElement;
        [SerializeField] private GameObject _elevatingChildElement;
        [SerializeField] private int _totalFloorCount;
        [SerializeField] private int _startFloorCount;
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
        }

        /// <summary>
        /// Build a world with various SerializedFields in Unity inspector.
        /// </summary>
        private void MakeFloors()
        {
            // set postion to Vector3.zero to make floors.
            transform.root.position = Vector3.zero;
            _elevatingTransform.position = Vector3.zero;
            _teleportableCount = _totalFloorCount - _startFloorCount;
            Vector3 refPosition = new Vector3(_elevatingChildElement.transform.position.x, 0f, _elevatingChildElement.transform.position.z);
            Quaternion refRotation = _elevatingChildElement.transform.rotation;
            float nextY = -((_startFloorCount - 1) * _floorGap) + _floorGapOffset;
            _rotatableWorldTransform.position = new Vector3(0f, nextY, 0f);
            _groundFloorElement.transform.position = _rotatableWorldTransform.position + new Vector3(refPosition.x, 0f, refPosition.z);
            for (int x = 1; _totalFloorCount > x; ++x)
            {
                nextY += _floorGap;
                Vector3 newPosition = new Vector3(refPosition.x, nextY, refPosition.z);
                GameObject spawnedGameObject = Instantiate(_elevatingChildElement, newPosition, refRotation, _elevatingTransform);
                spawnedGameObject.name = $"floor {x + 1} rail";
            }
            Destroy(_elevatingChildElement);
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
