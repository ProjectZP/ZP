using System;
using System.Collections;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.Villin.Teleport
{
    public class StartStageDoorController : DoorController
    {
        [SerializeField] private GameObject _transparentCollision;
        public Action OnStartStageDoorOpened;
        private PlayerManager _playerManager;
        private bool _isFirstOpen = true;

        protected override void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// Check Exception When <see cref="StairDoorController"/> Awake.
        /// </summary>
        protected override void CheckAwakeException()
        {
            base.CheckAwakeException();
            if (_playerManager == default)
            {
                _playerManager = FindFirstObjectByType<PlayerManager>();
            }
        }

        /// <summary>
        /// Set <see cref="Action"/>s using in <see cref="StartStageDoorController"/>
        /// </summary>
        protected override void SetActionSubscribers()
        {
            base.SetActionSubscribers();
            OnInteractDoor += DeactivateCollision;
            _playerManager.OnExitEndStageRegion += SubscribeOnExitEndStageRegion;
            _teleportManager.OnLeftTeleport += SubscribeOnteleport;
            _teleportManager.OnRightTeleport += SubscribeOnteleport;
        }

        protected override void SubscribeOnExitEndStageRegion()
        {
            base.SubscribeOnExitEndStageRegion();
            StartCoroutine(CloseDoorCoroutine());
        }
        private IEnumerator CloseDoorCoroutine()
        {
            yield return StartCoroutine(SetStateCoroutine(DoorStateList.DoorClose));
        }

        /// <summary>
        /// Start Coroutine to Activate Collision.
        /// </summary>
        private void ActivateCollision()
        {
            StartCoroutine(ActivateCollisionCoroutine());
        }

        /// <summary>
        /// Activate collision when DoorClose Aninmation is ended.
        /// </summary>
        /// <returns><see cref="SetStateCoroutine"/></returns>
        private IEnumerator ActivateCollisionCoroutine()
        {
            _transparentCollision.GetComponent<BoxCollider>().enabled = true;
            yield return StartCoroutine(SetStateCoroutine(DoorStateList.DoorClose));
        }

        /// <summary>
        /// Start Coroutine to Deactivate Collision.
        /// </summary>
        private void DeactivateCollision()
        {
            if (_isFirstOpen == false)
            {
                return;
            }
            OnStartStageDoorOpened?.Invoke();
            StartCoroutine(DeactivateCollisionCoroutine());
            _isFirstOpen = false;
        }

        private IEnumerator DeactivateCollisionCoroutine()
        {
#if UNITY_EDITOR
            Debug.Log("SetStateCoroutine Start");
#endif
            _transparentCollision.GetComponent<BoxCollider>().enabled = false;
            yield return StartCoroutine(SetStateCoroutine(DoorStateList.DoorOpen));
        }

        private void SubscribeOnteleport()
        {
            _isFirstOpen = true;
        }
    }
}
