using System;
using System.Collections;
using UnityEngine;
using ZP.SJH.Player;
using ZP.BHS.Zombie;

namespace ZP.Villin.Teleport
{
    public class RightEndStageDoorController : DoorController
    {
        [SerializeField] PlayerManager _playerManager;
        [SerializeField] private Collider _rightTransparentCollision;
        [SerializeField] private ZombieOnStairChecker _zombieOnStairChecker;


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
            if (_zombieOnStairChecker == default)
            {
                _zombieOnStairChecker = FindFirstObjectByType<ZombieOnStairChecker>();
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
            _isRightDoorActivating = false;
#if UNITY_EDITOR
            Debug.Log($"_isRightDoorActivating changed value to {_isRightDoorActivating} because of RightTeleport");
#endif
        }

        private void SubscribeOnLeftTeleport()
        {
            _isRightDoorActivating = true;
#if UNITY_EDITOR
            Debug.Log($"_isRightDoorActivating changed value to {_isRightDoorActivating} because of LeftTeleport");
#endif
        }


        /// <summary>
        /// Start Coroutine to Activate Collision.
        /// </summary>
        private void ActivateCollision()
        {
            if (_isPlayerOnEndStageRegion == false)
            {
#if UNITY_EDITOR
                Debug.Log("Player is not on Stair!");
#endif
                return;
            }
            if (_zombieOnStairChecker.IsLivingZombieOnStair == true)
            {
#if UNITY_EDITOR
                Debug.Log("Zombie is in Stair!");
#endif
                return;
            }
                StartCoroutine(ActivateCollisionCoroutine());
#if UNITY_EDITOR
                Debug.Log("RightEndStage Collision Activated");
#endif
        }

        /// <summary>
        /// Activate collision when DoorClose Aninmation is ended.
        /// </summary>
        /// <returns><see cref="SetStateCoroutine"/></returns>
        private IEnumerator ActivateCollisionCoroutine()
        {
            if (_isInteractable == false)
            {
                yield break;
            }
            if (_isRightDoorActivating == false)
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Warnig! _isRightDoorActivated is {_isRightDoorActivating}");
#endif
                yield break;
            }
#if UNITY_EDITOR
            Debug.Log($"_isRightDoorActivated is {_isRightDoorActivating}");
#endif
            _rightTransparentCollision.GetComponent<BoxCollider>().enabled = true;
            yield return StartCoroutine(SetStateCoroutine(DoorStateList.DoorClose));
            if (_zombieOnStairChecker.IsLivingZombieOnStair == true)
            {
                DeactivateCollision();
                yield break;
            }
            if (_teleportManager.GetNowRemainTeleportCount() == 0)
            {
                yield break;
            }

            DeactivateCollision();
            OnEndStageDoorClosed?.Invoke();
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
            _rightTransparentCollision.GetComponent<BoxCollider>().enabled = false;
        }

        private IEnumerator DeactivateCollisionCoroutine()
        {
            if (_isInteractable == false)
            {
                yield break;
            }
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
