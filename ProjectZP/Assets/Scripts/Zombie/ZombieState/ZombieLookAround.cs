using UnityEngine;

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
        private ZombieSightStateController _zombieSight;

        private void OnEnable()
        {
            _passedTime = 0;
            if (_zombieSight == null)
            {
                _zombieSight = GetComponentInChildren<ZombieSightStateController>();
                _zombieSight.OnPlayerGetInSight += PlayerFound;
            }
        }
        private void Update()
        {
            _passedTime += Time.deltaTime;
            if (_passedTime > _searchingTime)
            {
                zombieStateController.ChangeZombieState(ZombieStates.ZombieIdle);
            }
        }

        private void PlayerFound(Player player)
        {
            zombieStateController.ChangeZombieState(ZombieStates.ZombieChase);
        }
    }
}
