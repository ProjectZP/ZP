using UnityEngine;
using ZP.SJH.Player;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// Zombie Look Around to find Target.
    /// If can't go to Idle.
    /// </summary>
    class ZombieLookAround : ZombieState
    {
        private const float _searchingTime = 3f;
        private float _passedTime = 0;

        public ZombieLookAround(ZombieStateController zombieStateController) : base(zombieStateController)
        {
        }

        public override void OnStateEnter()
        {
            _passedTime = 0;
        }

        public override void OnStateUpdate()
        {
            _passedTime += Time.deltaTime;
            if (_passedTime > _searchingTime)
            {
                zombieStateController.ChangeZombieState(ZombieStates.ZombieIdle);
            }
        }

        public override void OnStateExit()
        {
            //
        }
    }
}
