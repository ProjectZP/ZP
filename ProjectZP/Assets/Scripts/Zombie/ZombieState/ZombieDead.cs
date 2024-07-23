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
            _zombieManager.gameObject.SetActive(false);
            //Todo:
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