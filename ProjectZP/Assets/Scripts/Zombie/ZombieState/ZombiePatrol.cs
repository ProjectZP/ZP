using Unity.Mathematics;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.BHS.Zombie
{
    class ZombiePatrol : ZombieState
    {
        private float _movingTime = 3;
        private float _passedTime = 0;
        private const float _movingSpeed = 3;

        private Vector3 _heading = Vector3.zero;

        public ZombiePatrol(ZombieStateController zombieStateController) : base(zombieStateController)
        {
        }

        public override void OnStateEnter()
        {
            int tempHeading = UnityEngine.Random.Range(0, 360);
            _heading.y = tempHeading;
            _zombieManager.transform.localRotation = quaternion.Euler(_heading);
            _passedTime = 0;
        }

        public override void OnStateUpdate()
        {
            _passedTime += Time.deltaTime;
            _zombieManager.transform.position += _zombieManager.transform.forward * _zombieManager.zombieStatus.WalkSpeed * Time.deltaTime;
            if (_passedTime >= _movingTime) { zombieStateController.ChangeZombieState(ZombieStates.ZombieIdle); }
        }

        public override void OnStateExit()
        {
            //
        }
    }
}