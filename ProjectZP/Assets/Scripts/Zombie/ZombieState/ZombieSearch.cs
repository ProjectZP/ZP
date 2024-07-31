using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// Zombie Move to Position Where Target Last seen.
    /// If zombie cant reach to the position in some reason, after few seconds its state changes into idle.
    /// </summary>
    class ZombieSearch : ZombieState
    {
        private Vector3 _searchPosition;
        private float _searchTime;
        private const float _searchEndTime = 10f;

        public ZombieSearch(ZombieStateController zombieStateController) : base(zombieStateController)
        {

        }

        public override void OnStateEnter()
        {
            _zombieAudioManager.PlayAudioClip(_zombieAudioManager.AudioClipAngry);

            _searchPosition = _zombieManager.targetposition;

            _agent.speed = _zombieManager.ZombieStatus.RunSpeed;
            _agent.isStopped = false;
            _agent.SetDestination(_searchPosition);
        }

        public override void OnStateUpdate()
        {
            _searchTime += Time.deltaTime;
            if (Vector3.Distance(
                _searchPosition, 
                _zombieManager.RefTransform.position) < 
                1f) 
            { 
                _zombieStateController.ChangeZombieState(ZombieStates.ZombieLookAround); 
            }

            if ( _searchTime > _searchEndTime)
            { 
                _zombieStateController.ChangeZombieState(ZombieStates.ZombieLookAround); 
            }
        }

        public override void OnStateExit()
        {
            //
        }
    }
}
