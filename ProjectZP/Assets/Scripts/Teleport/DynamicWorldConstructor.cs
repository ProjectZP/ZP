using UnityEngine;

namespace ZP.Villin.World
{
    public class DynamicWorldConstructor : MonoBehaviour
    {
        [SerializeField] private Transform _rotatableWorldTransform;
        [SerializeField] private Transform _elevatingTransform;
        [SerializeField] private GameObject _elevatingChildElement;
        [SerializeField] private GameObject _groundFloorElement;
        [SerializeField] private int _totalFloorCount;
        [SerializeField] private int _startFloorCount;
        [SerializeField] private float _floorGap;
        [SerializeField] private float _floorGapOffset;
        private int _teleportableCount;

        private void Awake()
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

            // set postion to Vector3.zero to make floors.
            _elevatingTransform.position = Vector3.zero;
            MakeFloors();
        }

        public float GetFloorGap()
        {
            return _floorGap;
        }

        public int GetTeleportableCount()
        {
            return _teleportableCount;
        }

        private void MakeFloors()
        {
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
    }
}
