using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// When Zombie Lost Target, This Enables.
    /// Zombie walk toward the point where target last seen.
    /// when Zombie arrive that point, state changes into LookAround.
    /// </summary>
    class ZombieSearch : ZombieState
    {
        Vector3 SearchPosition;

        private float searchTime;
        private float searchEndTime = 10f;

        public ZombieSearch(ZombieStateController zombieStateController) : base(zombieStateController)
        {

        }

        public override void OnStateEnter()
        {
            SearchPosition = _zombieManager.targetposition;

            _agent.speed = _zombieManager.zombieStatus.RunSpeed;
            _agent.isStopped = false;
            _agent.SetDestination(SearchPosition);
        }

        public override void OnStateUpdate()
        {
            searchTime += Time.deltaTime;
            if (Vector3.Distance(SearchPosition, _zombieManager.refTransform.position) < 1f) 
            { zombieStateController.ChangeZombieState(ZombieStates.ZombieLookAround); }

            if ( searchTime > searchEndTime)
            { zombieStateController.ChangeZombieState(ZombieStates.ZombieLookAround); }
        }

        public override void OnStateExit()
        {
            //
        }
    }
}
