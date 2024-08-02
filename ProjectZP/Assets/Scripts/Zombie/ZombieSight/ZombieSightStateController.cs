using System.Collections.Generic;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This class is Component of Zombie's Sight GameObject.
    /// Zombie's Runs Event When Player gets in Sight or gets out.
    /// </summary>
    class ZombieSightStateController : MonoBehaviour
    {
        public delegate void PlayerGetInSight(Camera player);
        public event PlayerGetInSight OnPlayerGetInSight;

        public delegate void PlayerGetOutSight(Camera player);
        public event PlayerGetOutSight OnPlayerGetOutSight;

        private ZombieSight _currentSight;
        private ZombieStateController _zombieStateController;
        private Dictionary<ZombieSightState, ZombieSight> _zombieSightStateDictionary;
        private ZombieSightState _currentSightState = ZombieSightState.None;

        private void Awake()
        {
            _zombieStateController  = transform.root.GetComponent<ZombieStateController>();

            InitZombieSightStateDictionary();
            _currentSight = _zombieSightStateDictionary[ZombieSightState.Idle];
        }

        private void Start()
        {
            _zombieStateController.OnZombieStateChanged += ChangeSightState;
        }

        private void Update()
        {
            _currentSight.OnSightUpdate();
        }

        private void InitZombieSightStateDictionary()
        {
            _zombieSightStateDictionary = new Dictionary<ZombieSightState, ZombieSight>
            {
                { ZombieSightState.Idle , new ZombieIdleSight(this) },
                { ZombieSightState.Chase, new ZombieChaseSight(this) }
            };
        }
        
        public void ChangeSightState(ZombieStates zombieState)
        {
            ZombieSightState temporarySight;

            _currentSight.OnSightExit();

            if (zombieState == ZombieStates.ZombieChase || 
                zombieState == ZombieStates.ZombieAttack)
            { 
                temporarySight = ZombieSightState.Chase; 
            }
            else 
            { 
                temporarySight = ZombieSightState.Idle; 
            }

            if(_currentSightState == temporarySight) 
            { 
                return; 
            }
            else 
            { 
                _currentSightState = temporarySight;
            }

            _currentSight = _zombieSightStateDictionary[_currentSightState];
            _currentSight.OnSightEnter();
        }

        /// <summary>
        /// This method Runs Event: OnPlayerGetInSight.
        /// </summary>
        /// <param name="player">Zombie's Target</param>
        public void FoundTarget(Camera player)
        {
            OnPlayerGetInSight(player);
        }
    }

    /// <summary>
    /// This Enum has Zombie's Sight State.
    /// </summary>
    public enum ZombieSightState
    {
        None = -1,
        Idle,
        Chase,
        Length,
    }
}
