using System.Diagnostics;

namespace ZP.BHS.Zombie
{
    class ZombieDead : ZombieState
    {
        public ZombieDead(ZombieStateController zombieStateController) : base(zombieStateController)
        {
        }

        public override void OnStateEnter()
        {
            _agent.isStopped = true; //Todo:
            _agent.enabled = false;
            zombieStateController.zombieAnimator.enabled = false;

            zombieSightStateController.enabled = false;
        }

        public override void OnStateUpdate()
        {
            //Todo:
        }

        public override void OnStateExit()
        {
            //Todo:
        }
    }
}