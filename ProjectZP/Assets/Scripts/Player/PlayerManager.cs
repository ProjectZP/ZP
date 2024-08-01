using System;
using UnityEngine;
using UnityEngine.Windows;

namespace ZP.SJH.Player
{
    [RequireComponent(typeof(PlayerStatusManager))]
    public class PlayerManager : MonoBehaviour
    {
        private const float RAYCAST_LENGTH = 2f;
        private readonly string LAYER_STAIR_RIGHT = "Stair";
        private readonly string LAYER_STAIR_LEFT = "StairLeft";

        public Action<Transform> OnEnterEndStageRegion;
        public Action OnExitEndStageRegion;
        public Action<float> OnPlayerDamaged;

        public PlayerStatusManager Status => _status;
        public PlayerInputManager Input => _input;
        public PlayerStateManager State => _state;

        private PlayerStatusManager _status;
        private PlayerInputManager _input;
        private PlayerStateManager _state;

        [SerializeField] private int _currentLayer;
        private int _stairLayerRight;
        private int _stairLayerLeft;

        private void Awake()
        {
            if (_status == null)
                _status = GetComponent<PlayerStatusManager>();
            if(_input == null)
                _input = transform.Find("Input Manager").GetComponent<PlayerInputManager>();
            if (_state == null)
                _state = transform.Find("State Manager").GetComponent<PlayerStateManager>();
            _stairLayerRight = LayerMask.NameToLayer(LAYER_STAIR_RIGHT);
            _stairLayerLeft = LayerMask.NameToLayer(LAYER_STAIR_LEFT);
            _status.LoadPlayerData();
        }

        private void Start()
        {
            OnPlayerDamaged += Status.OnPlayerDamaged;
        }

        private void Update()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, RAYCAST_LENGTH))
            {
                int hitLayer = hitInfo.collider.gameObject.layer;
                if (_currentLayer == hitLayer)
                    return;

                if (_currentLayer == _stairLayerRight && hitLayer != _stairLayerRight || _currentLayer == _stairLayerLeft && hitLayer != _stairLayerLeft)
                    OnExitEndStageRegion?.Invoke();

                _currentLayer = hitLayer;

                if (_currentLayer == _stairLayerRight || _currentLayer == _stairLayerLeft)
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

        public void TestFunc() => Debug.Log("TETSETET");

    }
}