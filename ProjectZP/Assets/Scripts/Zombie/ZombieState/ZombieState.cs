using UnityEngine.AI;

namespace ZP.BHS.Zombie
{
    abstract class ZombieState
    {
        public ZombieStates zombieState { get; protected set; }

        protected ZombieStateController         _zombieStateController;
        protected ZombieSightStateController    _zombieSightStateController;
        protected ZombieManager                 _zombieManager;
        protected NavMeshAgent                  _agent;
        protected ZombieAudioManager            _zombieAudioManager;

        public ZombieState(ZombieStateController zombieStateController)
        {
            _zombieStateController      = zombieStateController;
            _agent                      = zombieStateController.zombieAgent;
            _zombieManager              = zombieStateController.zombieManager;
            _zombieAudioManager         = zombieStateController.zombieAudioManager;
            _zombieSightStateController = zombieStateController.zombieSightStateController;
        }

        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        public abstract void OnStateExit();
    }
}
