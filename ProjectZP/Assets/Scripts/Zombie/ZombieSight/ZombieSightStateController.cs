using System;
using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This class is Component of Zombie's Sight GameObject.
    /// Zombie's Runs Event When Player gets in Sight or gets out.
    /// </summary>
    class ZombieSightStateController : MonoBehaviour //Todo: in this class, Using Player as "Player GameObject" but it's not accurate.
    {
        public delegate void PlayerGetInSight(Player player);
        public event PlayerGetInSight OnPlayerGetInSight;

        public delegate void PlayerGetOutSight(Player player);
        public event PlayerGetOutSight OnPlayerGetOutSight;

        private ZombieManager zombieManager;
        private ZombieStateController zombieStateController;

        private ZombieSightState currentSightState = ZombieSightState.None;
        
        private ZombieIdleSight ZombieIdleSight;
        private ZombieChasingSight ZombieChasingSight;


        private void OnEnable()
        {
            if (ZombieIdleSight == null) { ZombieIdleSight = GetComponent<ZombieIdleSight>(); }
            if (ZombieChasingSight == null) { ZombieChasingSight = GetComponent<ZombieChasingSight>(); }
            if (zombieManager == null) 
            { 
                zombieManager = transform.root.GetComponentInChildren<ZombieManager>();
            }
            if (zombieStateController == null) 
            { 
                zombieStateController = transform.root.GetComponentInChildren<ZombieStateController>();
                zombieStateController.OnZombieStateChanged += ChangeSightState;
            }
        }

        public void ChangeSightState(ZombieStates zombieState)
        {
            if(zombieState == ZombieStates.ZombieChase)
            {
                currentSightState = ZombieSightState.Chase;

                ZombieIdleSight.enabled = false;
                ZombieChasingSight.enabled = true;
            }
            else 
            { 
                currentSightState  = ZombieSightState.Idle;

                ZombieChasingSight.enabled = false;
                ZombieIdleSight.enabled = true;
            }
        }

        public void FoundTarget(Player player)
        {
            OnPlayerGetInSight(player);
        }
    }

    public enum ZombieSightState
    {
        None = -1,
        Idle,
        Chase,
        Length,

        Blind,
    }
}
