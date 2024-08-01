using System.Collections;
using Random = UnityEngine.Random;

namespace ZP.Villin.Teleport
{
    public class SingleDoorController : DoorController
    {
        private bool _isDoorOpened;


        protected override void Awake()
        {
            base.Awake();
            RandomOpenDoor();
        }




        protected override void SetActionSubscribers()
        {
            base.SetActionSubscribers();
            OnInteractDoor += SwitchDoorState;
            _teleportManager.OnRightTeleport += RandomOpenDoor;
            _teleportManager.OnLeftTeleport += RandomOpenDoor;
        }

        private void SwitchDoorState()
        {
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

        private void RandomOpenDoor()
        {
            _isDoorOpened = Random.Range(0f, 1f) >= 0.5f;
            SwitchDoorState();
        }
    }
}
