using System;
using System.Collections;
using UnityEngine;

namespace ZP.Villin.Teleport
{
    public class FinalStageDoorController : DoorController
    {
        public event Action GameClear;
        private bool _isDoorOpened;
        private bool _isGameCleared = false;


        protected override void SetActionSubscribers()
        {
            base.SetActionSubscribers();
            OnInteractDoor += SwitchDoorState;
        }

        private void SwitchDoorState()
        {
#if UNITY_EDITOR
            Debug.Log("SwitchDoorState has called!");
#endif
            if (_isGameCleared == true)
            {
#if UNITY_EDITOR
                Debug.Log("Do nothing because game has cleared!");
#endif
                return;
            }
            GameClear?.Invoke();
            if (_isDoorOpened == false)
            {
                StartCoroutine(OpenDoorCoroutine());
            }
            else
            {
                StartCoroutine(CloseDoorCoroutine());
            }
        }

        private IEnumerator CloseDoorCoroutine()
        {
            _isDoorOpened = false;
            yield return StartCoroutine(SetStateCoroutine(DoorStateList.DoorClose));
        }

        private IEnumerator OpenDoorCoroutine()
        {
            _isDoorOpened = true;
            yield return StartCoroutine(SetStateCoroutine(DoorStateList.DoorOpen));
        }
    }
}
