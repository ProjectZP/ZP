using System;
using System.Collections;
using UnityEngine;
using ZP.Villin.World;
using ZP.SJH.Player;

namespace ZP.Villin.Teleport
{
    /// <summary>
    /// Teleports player for simulate upstairs arrival.
    /// </summary>
    public class TeleportManager : MonoBehaviour
    {
        [SerializeField] private Transform _toTransform;
        [SerializeField] private Transform _elevatingTransform;
        [SerializeField] private Transform _rotatableWorldTransform;
        public Action OnRemainTeleportCountZero;
        public Action OnTeleport;
        private PlayerManager _playerManager;
        private DynamicWorldConstructor _dynamicWorldConstructor;
        private EndStageDoorController _endStageDoorController;
        private Vector3 _verticalMoveAmount;
        private Vector3 _teleportOffset = new Vector3 (0f, -2.5f, -15f);
        private int _nowRemainTeleportCount;
        private bool _isTeleportReady = false;
        private bool _isSyncCoroutineRunning = false;


        void Awake()
        {
            CheckAwakeException();
            SetMoveAmount();
            SetRemainTeleportCount();
            SetActionSubscribers();
        }

        /// <summary>
        /// Check Exception When <see cref="TeleportManager"/> Awake.
        /// </summary>
        private void CheckAwakeException()
        {
            if (_playerManager == default)
            {
                _playerManager = FindFirstObjectByType<PlayerManager>();
            }

            if (_endStageDoorController == default)
            {
                _endStageDoorController = FindFirstObjectByType<EndStageDoorController>();
            }

            if (_dynamicWorldConstructor == default)
            {
                _dynamicWorldConstructor = FindFirstObjectByType<DynamicWorldConstructor>();
            }
        }

        /// <summary>
        /// Set <see cref="_verticalMoveAmount"/> from <see cref="DynamicWorldConstructor.GetFloorGap()"/>.
        /// </summary>
        private void SetMoveAmount()
        {
            _verticalMoveAmount = new Vector3(0f, -(_dynamicWorldConstructor.GetFloorGap()), 0f);
        }

        /// <summary>
        /// Set <see cref="_nowRemainTeleportCount"/> from <see cref="DynamicWorldConstructor.GetTeleportableCount()"/>.
        /// </summary>
        private void SetRemainTeleportCount()
        {
            _nowRemainTeleportCount = _dynamicWorldConstructor.GetTeleportableCount();
        }

        /// <summary>
        /// Set <see cref="Action"/>s using in <see cref="TeleportManager"/>
        /// </summary>
        private void SetActionSubscribers()
        {
            _playerManager.OnEnterEndStageRegion += SubscribeOnEnterEndStageRegion;
            _playerManager.OnExitEndStageRegion += SubscribeOnExitEndStageRegion;
            _endStageDoorController.OnEndStageDoorClosed += SubscribeOnStairDoorClosed;
        }

        /// <summary>
        /// Start <see cref="SyncPositionCoroutine"/> when <see cref="PlayerManager.OnEnterEndStageRegion"/>&lt;<see cref="Transform"/>&gt; Invoked. 
        /// </summary>
        /// <param name="fromTransform">Player's <see cref="Transform"/></param>
        private void SubscribeOnEnterEndStageRegion(Transform fromTransform)
        {
#if UNITY_EDITOR
            Debug.Log("Action read success!");
#endif
            StartCoroutine(SyncPositionCoroutine(fromTransform));
        }


        /// <summary>
        /// Stop <see cref="SyncPositionCoroutine(Transform)"/> when <see cref="PlayerManager.OnExitEndStageRegion"/> Invoked.
        /// </summary>
        private void SubscribeOnExitEndStageRegion()
        {
            _isSyncCoroutineRunning = false;
        }

        /// <summary>
        /// Start <see cref="ExecuteTeleport(Transform)"/> when <see cref="StairDoorController.OnEndStageDoorClosed"/> is Invoked.
        /// </summary>
        private void SubscribeOnStairDoorClosed()
        {
            _isTeleportReady = true;
        }


        /// <summary>
        /// Synchronize the inverse position and rotation from <paramref name="fromTransform"/> to <see cref="_toTransform"/>.
        /// </summary>
        /// <param name="fromTransform">Player's <see cref="Transform"/> Invoked from <see cref="PlayerManager.OnEnterEndStageRegion"/>&lt;<see cref="Transform"/>&gt;</param>
        /// <returns></returns>
        private IEnumerator SyncPositionCoroutine(Transform fromTransform)
        {
            if (_isSyncCoroutineRunning == true)
            {
                yield break;
            }
                _isSyncCoroutineRunning = true;

#if UNITY_EDITOR
                Debug.Log("SyncPositionCoroutine Coroutine Started!");
#endif

            while (_isTeleportReady == false && _isSyncCoroutineRunning == true && _nowRemainTeleportCount > 0)
            {
                _isSyncCoroutineRunning = true;
                _toTransform.position = new Vector3(-fromTransform.position.x, fromTransform.position.y + _teleportOffset.y, -fromTransform.position.z + _teleportOffset.z);
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

            if (_isTeleportReady == true && _nowRemainTeleportCount == 0)
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
        /// <summary>
        /// Overwrite inverse position and rotation to <paramref name="fromTransform"/> for simulate upstairs arrival.
        /// </summary>
        private void ExecuteTeleport(Transform fromTransform)
        {
            if (_isTeleportReady == false)
            {
                return;
            }
            _nowRemainTeleportCount--;
            fromTransform.position = _toTransform.position;
            fromTransform.rotation = _toTransform.rotation;
            _elevatingTransform.position += _verticalMoveAmount;
            _rotatableWorldTransform.rotation = _rotatableWorldTransform.rotation * Quaternion.LookRotation(Vector3.back);
            OnTeleport?.Invoke();
#if UNITY_EDITOR
            Debug.Log($"Execute Teleport! = {fromTransform.position}");
#endif
        }

        /// <summary>
        /// Reset <see cref="_toTransform"/>.position and other private bools.
        /// </summary>
        private void ResetFields()
        {
            _toTransform.position = new Vector3(0f, 0f, 0f);
            _isTeleportReady = false;
            _isSyncCoroutineRunning = false;
        }
    }
}