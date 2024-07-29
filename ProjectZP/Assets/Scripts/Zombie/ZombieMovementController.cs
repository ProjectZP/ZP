using UnityEngine;
using UnityEngine.AI;

namespace ZP.BHS.Zombie
{
    class ZombieMovementController : MonoBehaviour
    {
        private ZombieManager _zombieManager;
        private ZombieStateController _zombieStateController;
        private NavMeshAgent _agent;



        private void Awake()
        {
            _zombieManager = GetComponent<ZombieManager>();
            _zombieStateController = GetComponent<ZombieStateController>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            
        }
    }
}
