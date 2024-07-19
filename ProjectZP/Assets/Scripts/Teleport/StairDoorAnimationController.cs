using System;
using UnityEngine;

namespace ZP.Villin.Teleport
{
    public class StairDoorAnimationController : MonoBehaviour
    {
        private ZP.Villin.Player.PlayerManager _playerManager;
        public Action OnStairDoorClosed;
        private TeleportManager _teleportManager;
        private GameObject _collision;
        private bool _isInteractable = false;
        private bool _isPlayerOnStair = false;


        private void Awake()
        {
            if (_teleportManager == default)
            {
                _teleportManager = FindFirstObjectByType<TeleportManager>();
            }
            _isInteractable = false;
            _playerManager.OnEnterStair += SubscribeOnEnterStair;
            _playerManager.OnExitStair += SubscribeOnExitStair;
            _teleportManager.OnRemainTeleportCountZero += MakeIntaractableUpstairDoor;
        }

        private void SubscribeOnExitStair()
        {
            _isPlayerOnStair = false;
        }

        private void SubscribeOnEnterStair(Transform transform)
        {

            _isPlayerOnStair = true;
        }

        private void MakeIntaractableUpstairDoor()
        {
            _isInteractable = true;
#if UNITY_EDITOR
            Debug.Log("Todo.Villin -> aMake Door Interactable!");
#endif
        }

        private void CloseDownstairDoor()
        {
            if (_isPlayerOnStair == false)
            {
                return;
            }

            _collision.SetActive(true);

            OnStairDoorClosed?.Invoke();
        }

        // 오류 안 보이도록 하는 임시 메서드.
        public void SetStairDoorClosed()
        {
            OnStairDoorClosed?.Invoke();
        }
    }
}
