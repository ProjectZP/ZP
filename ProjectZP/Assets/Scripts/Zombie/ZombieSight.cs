using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This class is Component of Zombie's Sight GameObject.
    /// Zombie's Runs Event When Player is in Sight.
    /// </summary>
    class ZombieSight : MonoBehaviour
    {
        public delegate void PlayerGetInSight(Player player);
        public event PlayerGetInSight OnPlayerGetInSight;

        public delegate void PlayerGetOutSight(Player player);
        public event PlayerGetOutSight OnPlayerGetOutSight;

    }
}
