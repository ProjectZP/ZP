using System;
using UnityEngine;

namespace ZP.Villin.Teleport
{
    public class StairDoorAnimationController : MonoBehaviour
    {
        public Action OnStairDoorClosed;
        private TeleportManager _teleportManager;
        private bool _isInteractable = false;


        private void Awake()
        {
            if (_teleportManager == default)
            {
                _teleportManager = FindFirstObjectByType<TeleportManager>();
            }
            _isInteractable = false;
            _teleportManager.OnRemainTeleportCountZero += MakeIntaractable;
        }

        private void MakeIntaractable()
        {
            _isInteractable = true;
#if UNITY_EDITOR
            Debug.Log("Todo.Villin -> aMake Door Interactable!");
#endif
        }

        // 오류 안 보이도록 하는 임시 메서드.
        public void SetStairDoorClosed()
        {
            OnStairDoorClosed?.Invoke();
        }
    }
}
