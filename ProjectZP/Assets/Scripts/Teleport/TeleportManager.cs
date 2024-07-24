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
            _endStageDoorController.OnEndStageDoorClosed += SubscribeOnEndStageDoorClosed;
            _endStageDoorController.OnEndStageDoorOpened += SubscribeOnEndStageDoorOpened;
        }

        /// <summary>
        /// Start <see cref="SyncPositionCoroutine"/> when <see cref="PlayerManager.OnEnterEndStageRegion"/>&lt;<see cref="Transform"/>&gt; Invoked. 
        /// </summary>
        /// <param name="fromTransform">Player's <see cref="Transform"/></param>
        private void SubscribeOnEnterEndStageRegion(Transform fromTransform)
        {
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
        private void SubscribeOnEndStageDoorClosed()
        {
            _isTeleportReady = true;
            Debug.Log($"OnEndStageDoor has closed {_isTeleportReady}");
        }

        private void SubscribeOnEndStageDoorOpened()
        {
            _isTeleportReady = false;
            Debug.Log($"OnEndStageDoor has opened {_isTeleportReady}");
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

            while (_isTeleportReady == false && _isSyncCoroutineRunning == true && _nowRemainTeleportCount > 0)
            {
                _toTransform.position = new Vector3(-fromTransform.position.x, fromTransform.position.y + _teleportOffset.y, -fromTransform.position.z + _teleportOffset.z);
                _toTransform.rotation = fromTransform.rotation * Quaternion.LookRotation(Vector3.back);

                yield return null;
            }

            if (_isTeleportReady == true && _nowRemainTeleportCount == 0)
            {
                OnRemainTeleportCountZero?.Invoke();
            }

            if (_isTeleportReady == true)
            {
                ExecuteTeleport(fromTransform);
            }

            ResetFields();
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