using System;
using UnityEngine;

namespace ZP.BHS.Zombie
{
    class ZombiePatrol : ZombieState
    {
        private float _movingTime;
        private float _passedTime = 0;
        private const float _minimumPatrolTime = 3f;
        private const float _maximumPatrolTime = 7f;

        private Vector3 _heading = Vector3.zero;

        public ZombiePatrol(ZombieStateController zombieStateController) : base(zombieStateController)
        {
        }

        public override void OnStateEnter()
        {
            _zombieAudioManager.PlayAudioClip(_zombieAudioManager.AudioClipIdle);

            _passedTime = 0;

            System.Random random = new System.Random();
            double angle = random.NextDouble() * 2 * Math.PI;

            double x = Math.Cos(angle) * 10;
            double z = Math.Sin(angle) * 10;

            _heading = new Vector3(
                _zombieManager.RefTransform.position.x + (float)x,
                _zombieManager.transform.position.y,
                _zombieManager.RefTransform.position.y + (float)z);

            _agent.isStopped = false;
            _agent.speed = _zombieManager.ZombieStatus.WalkSpeed;
            _agent.SetDestination(_heading);

            _movingTime = UnityEngine.Random.Range(_minimumPatrolTime,_maximumPatrolTime);
        }

        public override void OnStateUpdate()
        {
            _passedTime += Time.deltaTime;
            if (_passedTime >= _movingTime) 
            { 
                _zombieStateController.ChangeZombieState(ZombieStates.ZombieIdle); 
            }
        }

        public override void OnStateExit()
        {
        }
    }
}