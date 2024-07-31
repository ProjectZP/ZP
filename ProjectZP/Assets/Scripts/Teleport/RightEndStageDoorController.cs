using System;
using System.Collections;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.Villin.Teleport
{
    public class RightEndStageDoorController : DoorController
    {
        [SerializeField] PlayerManager _playerManager;
        [SerializeField] private Collider _rightTransparentCollision;


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
        /// Set <see cref="Action"/>s using in <see cref="RightEndStageDoorController"/>
        /// </summary>
        protected override void SetActionSubscribers()
        {
            base.SetActionSubscribers();
            OnInteractDoor += ActivateCollision;
            _playerManager.OnEnterEndStageRegion += SubscribeOnEnterEndStageRegion;
            _playerManager.OnExitEndStageRegion += SubscribeOnExitEndStageRegion;
            _teleportManager.OnRightTeleport += SubscribeOnRightTeleport;
            _teleportManager.OnLeftTeleport += SubscribeOnLeftTeleport;
            _playerManager.OnExitEndStageRegion += DeactivateCollision;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            DeactivateCollision();
        }

        protected override void SubscribeOnExitEndStageRegion()
        {
            base.SubscribeOnExitEndStageRegion();
            DeactivateCollision();
        }

        private void SubscribeOnRightTeleport()
        {
            _isRightDoorActivated = false;
#if UNITY_EDITOR
            Debug.Log($"_isRightDoorActivated{_isRightDoorActivated} at RightTeleport");
#endif
        }

        private void SubscribeOnLeftTeleport()
        {
            _isRightDoorActivated = true;
#if UNITY_EDITOR
            Debug.Log($"_isRightDoorActivated{_isRightDoorActivated} at LeftTeleport");
#endif
        }


        /// <summary>
        /// Start Coroutine to Activate Collision.
        /// </summary>
        private void ActivateCollision()
        {
            if (_isPlayerOnEndStageRegion == false)
            {
                return;
            }
            if (_isRightDoorActivated == true)
            {
                StartCoroutine(ActivateCollisionCoroutine());
#if UNITY_EDITOR
                Debug.Log("RightEndStage Collision Activated");
#endif
            }
        }

        /// <summary>
        /// Activate collision when DoorClose Aninmation is ended.
        /// </summary>
        /// <returns><see cref="SetStateCoroutine"/></returns>
        private IEnumerator ActivateCollisionCoroutine()
        {
            if (_isRightDoorActivated == false)
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Warnig! _isRightDoorActivated is {_isRightDoorActivated}");
#endif
                yield break;
            }
#if UNITY_EDITOR
            Debug.Log($"_isRightDoorActivated is {_isRightDoorActivated}");
#endif
            _rightTransparentCollision.GetComponent<BoxCollider>().enabled = true;
            yield return StartCoroutine(SetStateCoroutine(DoorStateList.DoorClose));
            OnEndStageDoorClosed?.Invoke();
            if (_teleportManager.GetNowRemainTeleportCount() == 0)
            {
                yield break;
            }

            DeactivateCollision();
#if UNITY_EDITOR
            Debug.Log("OnEndStageDoorClosed Invoked at RightEndStage");
#endif
        }

        /// <summary>
        /// Start Coroutine to Deactivate Collision.
        /// </summary>
        private void DeactivateCollision()
        {
            StartCoroutine(DeactivateCollisionCoroutine());
        }

        private IEnumerator DeactivateCollisionCoroutine()
        {
#if UNITY_EDITOR
            Debug.Log("SetStateCoroutine Start");
#endif
            _rightTransparentCollision.GetComponent<BoxCollider>().enabled = false;
            yield return StartCoroutine(SetStateCoroutine(DoorStateList.DoorOpen));
            OnEndStageDoorOpened?.Invoke();
#if UNITY_EDITOR
            Debug.Log("OnEndStageDoorOpened Invoked at RightEndStage");
#endif
        }

    }
}
