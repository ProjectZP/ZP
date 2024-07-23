using System.Collections;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// Zombie Chasing contains Two Phase.
    /// Phase 1. Rotate itself toward target found.
    /// Phase 2. Slowly move toward target. and rotate itself toward Target.
    /// </summary>
    class ZombieChase : ZombieState
    {
        private bool _rotateComplete = false;

        public ZombieChase(ZombieStateController zombieStateController) : base(zombieStateController)
        {
        }

        public override void OnStateEnter()
        {
            _rotateComplete = false;
        }

        public override void OnStateUpdate()
        {
            if (!_rotateComplete)
            {
                Vector3 targetvector = _zombieManager.Target.transform.position - _zombieManager.transform.position;
                targetvector.y = _zombieManager.transform.position.y;

                Quaternion targetRotation = Quaternion.LookRotation(targetvector);

                _zombieManager.transform.rotation = Quaternion.RotateTowards
                    (_zombieManager.transform.rotation, targetRotation,
                    _zombieManager.zombieStatus.RotationSpeed * Time.deltaTime * (1f / 6f));

                if (Vector3.Angle(targetvector,
                    _zombieManager.transform.forward) < 10)
                {
                    _rotateComplete = true;
                }
            }
            else
            {
                _zombieManager.targetposition = _zombieManager.Target.transform.position;

                Vector3 targetvector = _zombieManager.Target.transform.position - _zombieManager.transform.position;
                targetvector.y = _zombieManager.transform.position.y;

                Quaternion targetRotation = Quaternion.LookRotation(targetvector);
                _zombieManager.transform.rotation = Quaternion.RotateTowards
                    (_zombieManager.transform.rotation, targetRotation,
                    _zombieManager.zombieStatus.RotationSpeed * Time.deltaTime);

                _zombieManager.transform.position +=
                    _zombieManager.transform.forward * _zombieManager.zombieStatus.WalkSpeed * Time.deltaTime;


                if (Vector3.Distance(_zombieManager.Target.transform.position,
                    _zombieManager.transform.position) < _zombieManager.zombieStatus.AttackRange)
                { zombieStateController.ChangeZombieState(ZombieStates.ZombieAttack); }
            }
        }

        public override void OnStateExit()
        {
            //
        }
    }
}