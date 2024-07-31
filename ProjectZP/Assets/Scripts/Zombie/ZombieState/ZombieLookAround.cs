using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// Zombie Look Around to find Target.
    /// If can't go to Idle.
    /// </summary>
    class ZombieLookAround : ZombieState
    {
        private const float _searchingTime = 3f; //For 3 second, Zombie Look Around to Find Target.
        private float _passedTime = 0;

        public ZombieLookAround(ZombieStateController zombieStateController) : base(zombieStateController)
        {
        }

        public override void OnStateEnter()
        {
            _zombieAudioManager.PlayAudioClip(_zombieAudioManager.AudioClipSearch);

            _passedTime = 0;
        }

        public override void OnStateUpdate()
        {
            _passedTime += Time.deltaTime;
            if (_passedTime > _searchingTime)
            {
                _zombieStateController.ChangeZombieState(ZombieStates.ZombieIdle);
            }
        }

        public override void OnStateExit()
        {
        }
    }
}
