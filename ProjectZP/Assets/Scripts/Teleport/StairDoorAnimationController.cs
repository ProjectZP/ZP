using System;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.Villin.Teleport
{
    public class StairDoorAnimationController : MonoBehaviour
    {
        public Action OnStairDoorClosed;
        private PlayerManager _playerManager;
        private TeleportManager _teleportManager;
        private GameObject _collision;
        private bool _isInteractable = false;
        private bool _isPlayerOnStair = false;


        private void Awake()
        {
            CheckAwakeException();
            SetEventSubscribers();
        }

        /// <summary>
        /// Check Exception When <see cref="StairDoorAnimationController"/> Awake.
        /// </summary>
        private void CheckAwakeException()
        {
            if (_teleportManager == default)
            {
                _teleportManager = FindFirstObjectByType<TeleportManager>();
            }

            if (_playerManager == default)
            {
                _playerManager = FindFirstObjectByType<PlayerManager>();
            }
        }

        /// <summary>
        /// Set <see cref="Action"/>s using in <see cref="StairDoorAnimationController"/>
        /// </summary>
        private void SetEventSubscribers()
        {
            _playerManager.OnEnterStair += SubscribeOnEnterStair;
            _playerManager.OnExitStair += SubscribeOnExitStair;
            _teleportManager.OnRemainTeleportCountZero += MakeIntaractableUpstairDoor;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SubscribeOnExitStair()
        {
            _isPlayerOnStair = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform">Get player transform from <see cref="PlayerManager.OnEnterStair"/></param>
        private void SubscribeOnEnterStair(Transform transform)
        {

            _isPlayerOnStair = true;
        }

        /// <summary>
        /// OnProgress code. need more writing. Works when <see cref="TeleportManager.OnRemainTeleportCountZero"/> Invoked.
        /// </summary>
        private void MakeIntaractableUpstairDoor()
        {
            _isInteractable = true;
#if UNITY_EDITOR
            Debug.Log("Todo.Villin -> aMake Door Interactable!");
#endif
        }

        /// <summary>
        /// OnProgress code. need more writing. Works when push trigger on door collider.
        /// </summary>
        private void CloseDownstairDoor()
        {
            if (_isPlayerOnStair == false)
            {
                return;
            }

            _collision.SetActive(true);

            OnStairDoorClosed?.Invoke();
        }

        /// <summary>
        /// method for test. remove when complete code writing.
        /// </summary>
        public void SetStairDoorClosed()
        {
            OnStairDoorClosed?.Invoke();
        }
    }
}
