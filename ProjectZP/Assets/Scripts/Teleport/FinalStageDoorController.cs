using System;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.Villin.Teleport
{
    public class FinalStageDoorController : DoorController
    {
        private bool _isFinalStageDoorInteractable = false;


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
        /// Set <see cref="Action"/>s using in <see cref="EndStageDoorController"/>
        /// </summary>
        protected override void SetActionSubscribers()
        {
            base.SetActionSubscribers();
            _teleportManager.OnRemainTeleportCountZero += MakeIntaractableFinalStageDoor;
        }

        /// <summary>
        /// Set <see cref="_isPlayerOnEndStageRegion"/> true when OnEnterEndStageRegion Invoked.
        /// </summary>
        /// <param name="transform">Get player transform from <see cref="PlayerManager.OnEnterEndStageRegion"/></param>
        private void SubscribeOnEnterEndStageRegion(Transform transform)
        {

            _isPlayerOnEndStageRegion = true;
        }

        /// <summary>
        /// Set <see cref="_isPlayerOnEndStageRegion"/> false when OnExitEndStageRegion Invoked.
        /// </summary>
        private void SubscribeOnExitEndStageRegion()
        {
            _isPlayerOnEndStageRegion = false;
        }

        /// <summary>
        /// Actiave collision to prohibit player not go out specific region.
        /// </summary>
        protected override void DeactivateCollision()
        {
            if (_isFinalStageDoorInteractable == false)
            {
                return;
            }
            if (_isPlayerOnEndStageRegion == false)
            {
                return;
            }
            base.DeactivateCollision();
        }

        /// <summary>
        /// Make true <see cref="_isFinalStageDoorInteractable"/> when <see cref="TeleportManager.OnRemainTeleportCountZero"/> Invoked.
        /// </summary>
        private void MakeIntaractableFinalStageDoor()
        {
            _isFinalStageDoorInteractable = true;
        }
    }
}
