using System;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.Villin.Teleport
{
    public class EndStageDoorController : DoorController
    {


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
        }

        /// <summary>
        /// Actiave collision to prohibit player not go out specific region.
        /// </summary>
        public override void ActivateCollision()
        {
            if (_isPlayerOnEndStageRegion == false)
            {
                return;
            }
            base.ActivateCollision();
        }
    }
}
