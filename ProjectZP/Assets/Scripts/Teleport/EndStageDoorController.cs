using System;
using System.Collections;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.Villin.Teleport
{
    public class EndStageDoorController : DoorController
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
        /// Set <see cref="Action"/>s using in <see cref="EndStageDoorController"/>
        /// </summary>
        protected override void SetActionSubscribers()
        {
            base.SetActionSubscribers();
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
            base.ActivateCollision();
        }

        protected override IEnumerator ActivateCollisionCoroutine()
        {
            yield return base.ActivateCollisionCoroutine();
            OnEndStageDoorClosed?.Invoke();
        }


        /// <summary>
        /// Deactivate collision if player is not on Stair layer.
        /// </summary>
        /// 
        protected override void DeactivateCollision()
        {
            if ( _isPlayerOnEndStageRegion == true)
            {
                Debug.Log($"_isPlayerOnEndStageRegion = {_isPlayerOnEndStageRegion}");
                return;
            }
            base.DeactivateCollision();
        }

        protected override IEnumerator DeactivateCollisionCoroutine()
        {
            yield return base.DeactivateCollisionCoroutine();
            OnEndStageDoorOpened?.Invoke();
            Debug.Log("Invoked OnEndStageDoorOpened");
        }
    }
}
