using System.Security.Cryptography;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// Zombies stop, often move, but when you are in their sight They chase you.
    /// Or Die if they Attacked on back.
    /// </summary>
    class ZombieIdle : ZombieState
    {
        private float _passedTime = 0;
        private float _waitingTime = 0;
        private const float _waitingMinimum = 4;
        private const float _waitingMaximum = 6;
        private ZombieSightStateController _zombieSight;

        public ZombieIdle(ZombieStateController zombieStateController) : base(zombieStateController)
        {

        }

        public override void OnStateEnter()
        {
            _agent.isStopped = true;
            _waitingTime = Random.Range(_waitingMinimum, _waitingMaximum);
        }

        public override void OnStateUpdate()
        {
            _passedTime += Time.deltaTime;
            if (_passedTime > _waitingTime)
            {
                _passedTime = 0;
                zombieStateController.ChangeZombieState(ZombieStates.ZombiePatrol);
            }
        }

        public override void OnStateExit()
        {
            //
        }
    }
}