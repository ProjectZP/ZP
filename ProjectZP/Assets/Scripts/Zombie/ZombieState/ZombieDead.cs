using UnityEngine;

namespace ZP.BHS.Zombie
{
    class ZombieDead : ZombieState
    {
        float _deathTimer = 0;
        float deathTimer
        {
            get { return _deathTimer; }
            set
            {
                _deathTimer = value;
            }
        }

        public ZombieDead(ZombieStateController zombieStateController) : base(zombieStateController)
        {
        }

        public override void OnStateEnter()
        {
            for (int ix = 0; ix < zombieStateController.RagdollRigidbody.Length; ix++)
            {
                zombieStateController.RagdollRigidbody[ix].isKinematic = false;
                zombieStateController.RagdollRigidbody[ix].velocity = Vector3.zero;
            }

            _agent.isStopped = true;
            _agent.velocity = Vector3.zero;
            _agent.acceleration = 0f;
            _agent.updatePosition = false;
            _agent.updateRotation = false;
            _agent.enabled = false;

            zombieStateController.zombieAnimator.applyRootMotion = false;
            zombieStateController.zombieAnimator.enabled = false;



            zombieSightStateController.enabled = false;
        }

        public override void OnStateUpdate()
        {
            deathTimer += Time.deltaTime;
        }

        public override void OnStateExit()
        {
        }
    }
}