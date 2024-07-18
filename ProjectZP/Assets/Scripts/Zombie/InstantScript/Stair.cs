using System;
using UnityEngine;

namespace ZP.BHS.Zombie
{
    class Stair : MonoBehaviour
    {
        public delegate void DoorClose();
        public event DoorClose OnDoorClose;
    }
}
