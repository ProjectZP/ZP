using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This class is Component of Zombie's Sight GameObject.
    /// Zombie's Runs Event When Player gets in Sight or gets out.
    /// </summary>
    class ZombieSightStateController : MonoBehaviour //Todo: in this class, Using Player as "Player GameObject" but it's not accurate.
    {
        public delegate void PlayerGetInSight(PlayerManager player);
        public event PlayerGetInSight OnPlayerGetInSight;

        public delegate void PlayerGetOutSight(PlayerManager player);
        public event PlayerGetOutSight OnPlayerGetOutSight;

        private ZombieManager zombieManager;
        private ZombieStateController zombieStateController;
        private Dictionary<ZombieSightState, ZombieSight> zombieSightStateDictionary;
        private ZombieSight currentSight;

        

        //private ZombieIdleSight ZombieIdleSight;
        //private ZombieChasingSight ZombieChasingSight;

        private void Awake()
        {
            //if (ZombieIdleSight == null) { ZombieIdleSight = GetComponent<ZombieIdleSight>(); ZombieIdleSight.enabled = false; }
            //if (ZombieChasingSight == null) { ZombieChasingSight = GetComponent<ZombieChasingSight>(); ZombieChasingSight.enabled = false; }
            zombieManager = transform.root.GetComponentInChildren<ZombieManager>();
            zombieStateController = transform.root.GetComponentInChildren<ZombieStateController>();
            zombieStateController.OnZombieStateChanged += ChangeSightState;
            InitZombieSightStateDictionary();
            currentSight = zombieSightStateDictionary[ZombieSightState.Idle];
        }

        private void Update()
        {
            currentSight.OnSightUpdate();
        }

        private void InitZombieSightStateDictionary()
        {
            zombieSightStateDictionary = new Dictionary<ZombieSightState, ZombieSight>
            {
                { ZombieSightState.Idle, new ZombieIdleSight(this) },
                { ZombieSightState.Chase, new ZombieChaseSight(this) }
            };
        }

        private ZombieSightState currentSightState = ZombieSightState.None;
        ZombieSightState previousSightState = ZombieSightState.None;
        public void ChangeSightState(ZombieStates zombieState)
        {
            ZombieSightState tSight;

            currentSight.OnSightExit();
            if (zombieState == ZombieStates.ZombieChase || zombieState == ZombieStates.ZombieAttack)
            { tSight = ZombieSightState.Chase; }
            else { tSight = ZombieSightState.Idle; }

            if(currentSightState == tSight) { return; }
            else 
            { 
                previousSightState = currentSightState;
                currentSightState = tSight;
            }

            currentSight = zombieSightStateDictionary[currentSightState];
            currentSight.OnSightEnter();
        }

        public void FoundTarget(PlayerManager player)
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
