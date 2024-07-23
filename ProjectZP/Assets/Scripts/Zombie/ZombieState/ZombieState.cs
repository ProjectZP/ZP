using UnityEngine;

namespace ZP.BHS.Zombie
{
    abstract class ZombieState
    {
        public ZombieStates zombieState { get; protected set; }
        protected ZombieStateController zombieStateController;
        protected ZombieSightStateController zombieSightStateController;
        protected ZombieManager _zombieManager;

        public ZombieState(ZombieStateController zombieStateController)
        {
            this.zombieStateController = zombieStateController;
            zombieSightStateController = zombieStateController.zombieSightStateController;
            _zombieManager = zombieStateController.zombieManager;
        }

        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        public abstract void OnStateExit();
    }
}
