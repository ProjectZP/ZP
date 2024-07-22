using System;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.Villin.Teleport
{
    public class StartStageDoorController : DoorController
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
        /// Set <see cref="Action"/>s using in <see cref="StartStageDoorController"/>
        /// </summary>
        protected override void SetActionSubscribers()
        {
            base.SetActionSubscribers();
        }

        /// <summary>
        /// Dectiave collision to make player go out specific region.
        /// </summary>
        protected override void DeactivateCollision()
        {
            if (_isPlayerOnEndStageRegion == true)
            {
                return;
            }
            base.DeactivateCollision();
        }
    }
}
