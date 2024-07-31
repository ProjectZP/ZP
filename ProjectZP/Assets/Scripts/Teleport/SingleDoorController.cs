using System;
using System.Collections;
using TMPro;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.Villin.Teleport
{
    public class SingleDoorController : DoorController
    {
        private bool _isDoorOpened;


        protected override void SetActionSubscribers()
        {
            base.SetActionSubscribers();
            OnInteractDoor += SwitchDoorState;
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
    }
}
