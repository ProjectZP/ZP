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
        public ZombieChase(ZombieStateController zombieStateController) : base(zombieStateController)
        {
        }

        public override void OnStateEnter()
        {
            _agent.isStopped = false;
        }

        float reChaseTime = 0;
        public override void OnStateUpdate()
        {
            reChaseTime += Time.deltaTime;

            if (reChaseTime > 0.3f)
            {
                reChaseTime = 0f;
                _agent.SetDestination(_zombieManager.Target.transform.position);
            }

            if (Vector3.Distance(_zombieManager.Target.transform.position,
                    _zombieManager.transform.position) < _zombieManager.zombieStatus.AttackRange)
            { zombieStateController.ChangeZombieState(ZombieStates.ZombieAttack); }

            
        }

        public override void OnStateExit()
        {
            //
        }
    }
}