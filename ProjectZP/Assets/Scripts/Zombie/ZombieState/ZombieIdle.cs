using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// Zombies stop, often move, but when you are in their sight They chase you.
    /// Or Die if they Attacked on back.
    /// </summary>
    class ZombieIdle : ZombieState
    {
        private float _passedTime = 0;
        private float _waitingTime;
        private const float _waitingMinimum = 3;
        private const float _waitingMaximum = 7;

        public ZombieIdle(ZombieStateController zombieStateController) : base(zombieStateController)
        {

        }

        public override void OnStateEnter()
        {
            _zombieAudioManager.PlayAudioClip(_zombieAudioManager.AudioClipIdle);

            _agent.isStopped = true;

            _waitingTime = Random.Range(_waitingMinimum, _waitingMaximum);
        }

        public override void OnStateUpdate()
        {
            _passedTime += Time.deltaTime;
            if (_passedTime > _waitingTime)
            {
                _passedTime = 0;
                _zombieStateController.ChangeZombieState(ZombieStates.ZombiePatrol);
            }
        }

        public override void OnStateExit()
        {
        }
    }
}