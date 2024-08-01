using System;
using System.Collections;
using UnityEngine;
using ZP.BHS.Zombie;
using ZP.SJH.Player;

namespace ZP.Villin.Teleport
{
    public class LeftEndStageDoorController : DoorController
    {
        [SerializeField] PlayerManager _playerManager;
        [SerializeField] private Collider _leftTransparentCollision;
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
        /// Set <see cref="Action"/>s using in <see cref="LeftEndStageDoorController"/>
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
            if (_zombieOnStairChecker.IsLivingZombieOnStair == true)
            {
#if UNITY_EDITOR
                Debug.Log("Zombie is in Stair!");
#endif
                return;
            }
            if (_isRightDoorActivated == true)
            {
                return;
            }
            StartCoroutine(ActivateCollisionCoroutine());
#if UNITY_EDITOR
                Debug.Log("LeftEndStage Collision Activated");
# endif
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
            if (_isRightDoorActivated == true)
            {
#if UNITY_EDITOR
            Debug.Log($"_isRightDoorActivated is {_isRightDoorActivated}. So break Coroutine.");
#endif
                yield break;
            }
            _leftTransparentCollision.GetComponent<BoxCollider>().enabled = true;
            yield return StartCoroutine(SetStateCoroutine(DoorStateList.DoorClose));
            OnEndStageDoorClosed?.Invoke();
            if (_teleportManager.GetNowRemainTeleportCount() == 0)
            {
                yield break;
            }
            DeactivateCollision();
#if UNITY_EDITOR
            Debug.Log("OnEndStageDoorClosed Invoked at LeftEndStage");
#endif
        }

        /// <summary>
        /// Start Coroutine to Deactivate Collision.
        /// </summary>
        private void DeactivateCollision()
        {
            StartCoroutine(DeactivateCollisionCoroutine());
            _leftTransparentCollision.GetComponent<BoxCollider>().enabled = false;
        }

        private IEnumerator DeactivateCollisionCoroutine()
        {
            if (_isInteractable == false)
            {
                yield break;
            }
            yield return StartCoroutine(SetStateCoroutine(DoorStateList.DoorOpen));
            OnEndStageDoorOpened?.Invoke();
#if UNITY_EDITOR
            Debug.Log("OnEndStageDoorOpened Invoked at LeftEndStage");
#endif
        }
    }
}
