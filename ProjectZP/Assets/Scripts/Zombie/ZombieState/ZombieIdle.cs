using System.Security.Cryptography;
using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// Zombies stop, often move, but when you are in their sight They chase you.
    /// Or Die if they Attacked on back.
    /// </summary>
    class ZombieIdle : ZombieState, IZombieFoundPlayer
    {
        private float _passedTime = 0;
        private int _waitingTime = 0;
        private const int _waitingMinimum = 2;
        private const int _waitingMaximum = 6;
        private ZombieSightStateController _zombieSight;

        private void OnEnable()
        {
            if (_zombieSight == null) 
            { 
                _zombieSight = GetComponentInChildren<ZombieSightStateController>();
                _zombieSight.OnPlayerGetInSight += FoundPlayer;
            }

            _waitingTime = Random.Range(_waitingMinimum,_waitingMaximum);
        }

        private void Update()
        {
            _passedTime += Time.deltaTime;
            if(_passedTime > _waitingTime)
            {
                _passedTime = 0;
                zombieStateController.ChangeZombieState(ZombieStates.ZombiePatrol);
            }
        }

        //This method runs when Zombie FoundPlayer
        public void FoundPlayer(Player player)
        {
            zombieStateController.ChangeZombieState(ZombieStates.ZombieChase);
        }
    }
}