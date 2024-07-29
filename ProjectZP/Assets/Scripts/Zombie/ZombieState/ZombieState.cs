using UnityEngine;
using UnityEngine.AI;

namespace ZP.BHS.Zombie
{
    abstract class ZombieState
    {
        public ZombieStates zombieState { get; protected set; }
        protected ZombieStateController zombieStateController;
        protected ZombieSightStateController zombieSightStateController;
        protected ZombieManager _zombieManager;
        protected NavMeshAgent _agent;

        public ZombieState(ZombieStateController zombieStateController)
        {
            this.zombieStateController = zombieStateController;
            zombieSightStateController = zombieStateController.zombieSightStateController;
            _zombieManager = zombieStateController.zombieManager;
            _agent = zombieStateController.zombieAgent;
        }

        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        public abstract void OnStateExit();
    }
}
