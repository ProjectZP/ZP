using System;
using UnityEngine;

namespace ZP.SJH.Player
{
    [RequireComponent(typeof(PlayerStatusManager))]
    public class PlayerManager : MonoBehaviour
    {
        private const float RAYCAST_LENGTH = 2f;
        private readonly string LAYER_STAIR = "Stair";

        public Action<Transform> OnEnterEndStageRegion;
        public Action OnExitEndStageRegion;
        public Action<float> OnPlayerDamaged;

        public PlayerStatusManager Status => _status;
        

        private PlayerStatusManager _status;
        [SerializeField] private int _currentLayer;
        private int _stairLayer;

        private void Awake()
        {
            if(_status == null)
                _status = GetComponent<PlayerStatusManager>();

            _stairLayer = LayerMask.NameToLayer(LAYER_STAIR);
            _status.LoadPlayerData();
        }

        private void Update()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, RAYCAST_LENGTH))
            {
//#if UNITY_EDITOR
//                Debug.DrawLine(transform.position, hitInfo.point, Color.red);
//#endif

                int hitLayer = hitInfo.collider.gameObject.layer;
                if (_currentLayer == hitLayer)
                    return;

                if(_currentLayer == _stairLayer && hitLayer != _stairLayer)
                    OnExitEndStageRegion?.Invoke();

                _currentLayer = hitLayer;

                if (_currentLayer == _stairLayer)
                    OnEnterEndStageRegion?.Invoke(transform);
            }
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * RAYCAST_LENGTH);
#endif
        }

    }
}