using System;
using System.Collections;

namespace ZP.Villin.Teleport
{
    public class FinalStageDoorController : DoorController
    {
        public event Action GameClear;
        private bool _isFinalStageDoorInteractable = false;
        private bool _isDoorOpened;
        private bool _isGameCleared;


        protected override void SetActionSubscribers()
        {
            base.SetActionSubscribers();
            OnInteractDoor += SwitchDoorState;
        }

        private void SwitchDoorState()
        {
            if (_isGameCleared == true)
            {
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
            _isGameCleared = true;
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
