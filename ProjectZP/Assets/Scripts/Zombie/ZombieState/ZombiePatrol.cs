using System;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace ZP.BHS.Zombie
{
    class ZombiePatrol : ZombieState
    {
        private float _movingTime;
        private float _passedTime = 0;
        private const float _movingSpeed = 3;

        private Vector3 _heading = Vector3.zero;

        public ZombiePatrol(ZombieStateController zombieStateController) : base(zombieStateController)
        {
        }

        public override void OnStateEnter()
        {
            _passedTime = 0;
            System.Random random = new System.Random();
            double angle = random.NextDouble() * 2 * Math.PI;

            double x = Math.Cos(angle) * 10;
            double z = Math.Sin(angle) * 10;
            _heading = new Vector3(
                _zombieManager.refTransform.position.x + (float)x,
                _zombieManager.refTransform.position.y,
                _zombieManager.refTransform.position.y + (float)z);

            _agent.isStopped = false;
            _agent.SetDestination(_heading);

            _movingTime = UnityEngine.Random.Range(3f,7f);
        }

        public override void OnStateUpdate()
        {
            //_zombieManager.transform.position += _zombieManager.transform.forward * _zombieManager.zombieStatus.WalkSpeed * Time.deltaTime;

            _passedTime += Time.deltaTime;
            if (_passedTime >= _movingTime) { zombieStateController.ChangeZombieState(ZombieStates.ZombieIdle); }
        }

        public override void OnStateExit()
        {
            //
        }
    }
}