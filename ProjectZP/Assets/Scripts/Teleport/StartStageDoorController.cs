using System;
using System.Collections;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.Villin.Teleport
{
    public class StartStageDoorController : DoorController
    {
        public Action OnStartStageDoorOpened;

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
        /// Set <see cref="Action"/>s using in <see cref="StartStageDoorController"/>
        /// </summary>
        protected override void SetActionSubscribers()
        {
            base.SetActionSubscribers();
            _playerManager.OnExitEndStageRegion += SubscribeOnExitEndStageRegion;
        }

        protected override void SubscribeOnExitEndStageRegion()
        {
            base.SubscribeOnExitEndStageRegion();
            ActivateCollision();
        }

        public void TestOpenDoor()
        {
            DeactivateCollision();
        }

        /// <summary>
        /// Dectiave collision to make player go out specific region.
        /// </summary>
        public override void DeactivateCollision()
        {
            OnStartStageDoorOpened?.Invoke();
            base.DeactivateCollision();
        }

        protected override IEnumerator DeactivateCollisionCoroutine()
        {
            yield return base.DeactivateCollisionCoroutine();
        }
    }
}
