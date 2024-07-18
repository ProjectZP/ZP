using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using ZP.Villin.Player;

namespace ZP.Villin.Teleport
{
    public class TeleportManager : MonoBehaviour
    {
        [SerializeField] private Transform _toTransform;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private StairDoorAnimationController _stairDoorAnimationController;
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

            _teleportOffset.x = 0f;
            _teleportOffset.y = -2.5f;
            _teleportOffset.z = -15f;

            // 각 이벤트 구독.
            _playerManager.OnEnterStair += SubscribeOnEnterStair;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromTransform">Player's <see cref="Transform"/> Invoked from <see cref="PlayerManager.OnEnterStair"/>&lt;<see cref="Transform"/>&gt;</param>
        /// <returns></returns>
        private IEnumerator SyncPositionCoroutine(Transform fromTransform)
        {
#if UNITY_EDITOR
            Debug.Log("SyncPositionCoroutine Coroutine Started!");
#endif
            while (_isTeleportReady == false)
            {
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
            ExecuteTeleport(fromTransform);
            yield break;
        }

        private void SubscribeOnStairDoorClosed()
        {
            _isTeleportReady = true;
        }

        // 만일 오른쪽 계단 문 닫는 애니메이션이 종료되었다면 _fromTransform을 왼쪽 계단으로 옮기는 메서드.
        /// <summary>
        /// Teleport _fromTransform to _toTransform.position.
        /// </summary>
        private void ExecuteTeleport(Transform fromTransform)
        {
            if (_isTeleportReady == false)
            {
                return;
            }

            // _fromTransform.position을 _toTransform.position으로 변경.
            fromTransform.position = _toTransform.position;
            // _fromTransform.rotation을 _toTransform.rotation으로 변경.
            fromTransform.rotation = _toTransform.rotation;

            // 필드 초기화.
            _toTransform = null;
            _playerManager = null;
            _isTeleportReady = false;
        }
    }
}