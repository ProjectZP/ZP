using System;
using System.Collections;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.Villin.Teleport
{
    public class LeftEndStageDoorController : DoorController
    {
        public Action OnEndStageDoorClosed;
        public Action OnEndStageDoorOpened;

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
        }

        /// <summary>
        /// Set <see cref="Action"/>s using in <see cref="LeftEndStageDoorController"/>
        /// </summary>
        protected override void SetActionSubscribers()
        {
            base.SetActionSubscribers();
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


        /// <summary>
        /// Actiave collision to prohibit player not go out Stair layer region.
        /// </summary>
        public override void ActivateCollision()
        {
            if (_isPlayerOnEndStageRegion == false)
            {
                return;
            }
            if (_isRightDoorActivated == false)
            {
                base.ActivateCollision();
#if UNITY_EDITOR
                Debug.Log("LeftEndStage Collision Activated");
# endif
            }
        }

        protected override IEnumerator ActivateCollisionCoroutine()
        {
            if (_isRightDoorActivated == true)
            {
                yield break;
            }
#if UNITY_EDITOR
            Debug.Log($"_isRightDoorActivated is {_isRightDoorActivated}");
#endif
            yield return base.ActivateCollisionCoroutine();
            OnEndStageDoorClosed?.Invoke();
            DeactivateCollision();
#if UNITY_EDITOR
            Debug.Log("OnEndStageDoorClosed Invoked at LeftEndStage");
#endif
        }

        protected override void SubscribeOnExitEndStageRegion()
        {
            base.SubscribeOnExitEndStageRegion();
            DeactivateCollision();
        }

        /// <summary>
        /// Deactivate collision if player is not on Stair layer.
        /// </summary>
        /// 
        public override void DeactivateCollision()
        {
                base.DeactivateCollision();
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

        protected override IEnumerator DeactivateCollisionCoroutine()
        {
            yield return base.DeactivateCollisionCoroutine();
            OnEndStageDoorOpened?.Invoke();
#if UNITY_EDITOR
            Debug.Log("OnEndStageDoorOpened Invoked at LeftEndStage");
#endif
        }
    }
}
