using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using ZP.Villin.Player;
using ZP.Villin.World;
using PlayerManager = ZP.SJH.Player.PlayerManager;

namespace ZP.Villin.Teleport
{
    public class TeleportManager : MonoBehaviour
    {
        public Action OnRemainTeleportCountZero;
        [SerializeField] private Transform _toTransform;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private StairDoorAnimationController _stairDoorAnimationController;
        [SerializeField] private DynamicWorldConstructor _dynamicWorldConstructor;
        [SerializeField] private Transform _rotatableWorldTransform;
        [SerializeField] private Transform _elevatingTransform;
        private int _nowRemainTeleportCount;
        private Vector3 _verticalMoveAmount;
        bool _isSyncCoroutineRunning = false;
        private bool _isTeleportReady;
        private Vector3 _teleportOffset = Vector3.zero;


        void Awake()
        {
            // 예외 상황 확인. 혹시 더 좋은 방법 있다면 추후 수정.
            if (_playerManager == default)
            {
                _playerManager = FindFirstObjectByType<PlayerManager>();
            }

            if (_stairDoorAnimationController == default)
            {
                _stairDoorAnimationController = FindFirstObjectByType<StairDoorAnimationController>();
            }

            if (_dynamicWorldConstructor == default)
            {
                _dynamicWorldConstructor = FindFirstObjectByType<DynamicWorldConstructor>();
            }



            _teleportOffset.x = 0f;
            _teleportOffset.y = -2.5f;
            _teleportOffset.z = -15f;

            // 건물 층고에 따른 플레이어 텔레포트 시 필요한 y값 생성.
            _verticalMoveAmount = new Vector3(0f, -(_dynamicWorldConstructor.GetFloorGap()), 0f);

            // 텔레포트 가능 횟수 받아오기.
            _nowRemainTeleportCount = _dynamicWorldConstructor.GetTeleportableCount();

            // 각 이벤트 구독.
            _playerManager.OnEnterStair += SubscribeOnEnterStair;
            _playerManager.OnExitStair += SubscribeOnExitStair;
            _stairDoorAnimationController.OnStairDoorClosed += SubscribeOnStairDoorClosed;
        }


        // 플레이어가 stair 레이어에 진입했을 때 발생하는 이벤트를 받을 메서드.
        /// <summary>
        /// When <see cref="PlayerManager.OnEnterStair"/>&lt;<see cref="Transform"/>&gt; Invoked, Start <see cref="SyncPositionCoroutine"/>.
        /// </summary>
        /// <param name="fromTransform">Player's transform</param>
        private void SubscribeOnEnterStair(Transform fromTransform)
        {
#if UNITY_EDITOR
            Debug.Log("Action read success!");
#endif
            StartCoroutine(SyncPositionCoroutine(fromTransform));
        }

        private void SubscribeOnExitStair()
        {
            _isSyncCoroutineRunning = false;
        }

        /// <summary>
        /// Synchronize position of -fromTransform.position to _toTransform.position and reverse LookRotation.
        /// </summary>
        /// <param name="fromTransform">Player's <see cref="Transform"/> Invoked from <see cref="PlayerManager.OnEnterStair"/>&lt;<see cref="Transform"/>&gt;</param>
        /// <returns></returns>
        private IEnumerator SyncPositionCoroutine(Transform fromTransform)
        {
#if UNITY_EDITOR
            Debug.Log("SyncPositionCoroutine Coroutine Started!");
#endif
            _isSyncCoroutineRunning = true;
            while (_isTeleportReady == false && _isSyncCoroutineRunning == true && _nowRemainTeleportCount > 0)
            {
                _isSyncCoroutineRunning = true;
                _toTransform.position = new Vector3 (-fromTransform.position.x, fromTransform.position.y + _teleportOffset.y, -fromTransform.position.z + _teleportOffset.z);
#if UNITY_EDITOR
                Debug.Log($"fromTransform.position = {fromTransform.position}");
                Debug.Log($"_toTransform.position = {_toTransform.position}");
#endif
                _toTransform.rotation = fromTransform.rotation * Quaternion.LookRotation(Vector3.back);
#if UNITY_EDITOR
                Debug.Log($"fromTransform.rotation = {fromTransform.rotation}");
                Debug.Log($"_toTransform.rotation = {_toTransform.rotation}");
#endif
                yield return null;
            }

            if (_isTeleportReady == true)
            {
#if UNITY_EDITOR
                Debug.Log($"Trying Execute Teleport!");
#endif
                ExecuteTeleport(fromTransform);
            }

            if ( _isTeleportReady == true && _nowRemainTeleportCount == 0)
            {
#if UNITY_EDITOR
                Debug.Log($"Trying OnRemainTeleportCountZero Action Invoke!");
#endif
                OnRemainTeleportCountZero?.Invoke();
            }

#if UNITY_EDITOR
            Debug.Log($"Resetting Fields!");
#endif
            ResetFields();

#if UNITY_EDITOR
            Debug.Log($"Ending Coroutine!");
#endif
            yield break;
        }

        private void SubscribeOnStairDoorClosed()
        {
            _isTeleportReady = true;
        }

        // 만일 오른쪽 계단 문 닫는 애니메이션이 종료되었다면 _fromTransform을 왼쪽 계단으로 옮기는 메서드.
        /// <summary>
        /// Teleport fromTransform to _toTransform.position and Turn word 180 degree, move world down to trick player feels like arrived upstair floor.
        /// </summary>
        private void ExecuteTeleport(Transform fromTransform)
        {
            if (_isTeleportReady == false)
            {
                return;
            }
            _nowRemainTeleportCount--;
            // _fromTransform.position을 _toTransform.position으로 변경.
            fromTransform.position = _toTransform.position;
            // _fromTransform.rotation을 _toTransform.rotation으로 변경.
            fromTransform.rotation = _toTransform.rotation;
            _elevatingTransform.position += _verticalMoveAmount;
            _rotatableWorldTransform.rotation = _rotatableWorldTransform.rotation * Quaternion.LookRotation(Vector3.back);
#if UNITY_EDITOR
            Debug.Log($"Execute Teleport! = {fromTransform.position}");
#endif
        }

        private void ResetFields()
        {
            _toTransform.position = new Vector3(0f, 0f, 0f);
            _isTeleportReady = false;
            _isSyncCoroutineRunning = false;
        }


    }
}