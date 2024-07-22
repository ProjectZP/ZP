using System;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.Villin.Teleport
{
    [RequireComponent(typeof(DoorAnimationController))]
    public abstract class DoorController : MonoBehaviour
    {

        public Action OnEndStageDoorClosed;
        [SerializeField] private GameObject _collision;
        protected DoorAnimationController _controller;
        protected PlayerManager _playerManager;
        protected TeleportManager _teleportManager;
        protected bool _isPlayerOnEndStageRegion;


        protected virtual void Awake()
        {
            CheckAwakeException();
            SetActionSubscribers();
        }

        /// <summary>
        /// Check Exception When <see cref="StairDoorController"/> Awake.
        /// </summary>
        protected virtual void CheckAwakeException()
        {
            if (_teleportManager == default)
            {
                _teleportManager = FindFirstObjectByType<TeleportManager>();
            }

            if (_playerManager == default)
            {
                _playerManager = FindFirstObjectByType<PlayerManager>();
            }

            if (_controller == default)
            {
                _controller = GetComponent<DoorAnimationController>();
            }
        }

        /// <summary>
        /// Set <see cref="Action"/>s using in <see cref="StairDoorController"/>
        /// </summary>
        protected virtual void SetActionSubscribers()
        {
            _playerManager.OnEnterEndStageRegion += SubscribeOnEnterEndStageRegion;
            _playerManager.OnExitEndStageRegion += SubscribeOnExitEndStageRegion;
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
        public virtual void ActivateCollision()
        {
            _collision.SetActive(true);
        }

        /// <summary>
        /// Dectiave collision to make player go out StartStageRegion.
        /// </summary>
        protected virtual void DeactivateCollision()
        {
            _collision.SetActive(false);
        }
    }
}
