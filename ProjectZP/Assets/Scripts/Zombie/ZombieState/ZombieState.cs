using UnityEngine;

namespace ZP.BHS.Zombie
{
    class ZombieState : MonoBehaviour
    {
        public ZombieStates zombieState { get; protected set; }
        protected ZombieStateController zombieStateController;
        protected ZombieManager _zombieManager;

        private void Awake()
        {
            _zombieManager = GetComponent<ZombieManager>();
            zombieStateController = GetComponent<ZombieStateController>();
        }
    }
}
