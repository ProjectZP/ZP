using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// While State Zombie Search, Zombie Shake its head To Search Player.
    /// If it Ends, turn to idle.
    /// </summary>
    class ZombieSearch : ZombieState
    {
        private float _searchingTime = 3f;
        private float _passedTime = 0;
        private ZombieSight _zombieSight;

        private void OnEnable()
        {
            _passedTime = 0;
            if(_zombieSight == null)
            {
                _zombieSight = GetComponent<ZombieSight>();
                _zombieSight.OnPlayerGetInSight += PlayerFound;
            }
        }
        private void Update()
        {
            _passedTime += Time.deltaTime;
            if(_passedTime > _searchingTime)
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
