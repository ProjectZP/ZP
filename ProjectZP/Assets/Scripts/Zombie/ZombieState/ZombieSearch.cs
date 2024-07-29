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

        public ZombieSearch(ZombieStateController zombieStateController) : base(zombieStateController)
        {

        }

        public override void OnStateEnter()
        {
            SearchPosition = _zombieManager.targetposition;
            SearchPosition.y = _zombieManager.transform.position.y;

            _agent.isStopped = false;
            _agent.SetDestination(SearchPosition);
        }

        public override void OnStateUpdate()
        {
            if (Vector3.Distance(SearchPosition, _zombieManager.transform.position) < 0.2f) 
            { zombieStateController.ChangeZombieState(ZombieStates.ZombieLookAround); }
        }

        public override void OnStateExit()
        {
            //
        }
    }
}
