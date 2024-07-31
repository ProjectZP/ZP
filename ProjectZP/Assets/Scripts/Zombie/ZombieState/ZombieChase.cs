using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// Chase Target.
    /// </summary>
    class ZombieChase : ZombieState
    {
        public ZombieChase(ZombieStateController zombieStateController) : base(zombieStateController)
        {
        }

        public override void OnStateEnter()
        {
            _zombieAudioManager.PlayAudioClip(_zombieAudioManager.AudioClipAngry);

            _agent.isStopped = false;
            _agent.speed = _zombieManager.ZombieStatus.RunSpeed;
        }

        float reChaseTime = 0;

        /// <summary>
        /// Chase Target in every 0.3f seconds.
        /// </summary>
        public override void OnStateUpdate()
        {
            reChaseTime += Time.deltaTime;

            if (reChaseTime > 0.3f)
            {
                reChaseTime = 0f;
                _agent.SetDestination(_zombieManager.Target.transform.position);
            }

            if (Vector3.Distance(
                _zombieManager.Target.transform.position,
                _zombieManager.RefTransform.position) < 
                _zombieManager.ZombieStatus.AttackRange)
            { 
                _zombieStateController.ChangeZombieState(ZombieStates.ZombieAttack); 
            }
        }

        public override void OnStateExit()
        {
        }
    }
}