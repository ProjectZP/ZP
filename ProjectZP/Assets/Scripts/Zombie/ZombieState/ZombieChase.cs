using UnityEngine;

namespace ZP.BHS.Zombie
{
    class ZombieChase : ZombieState
    {
        private const float _movingSpeed = 10;
        private ZombieSight _zombieSight;
        private void OnEnable()
        {
            if (_zombieSight == null) 
            { 
                _zombieSight = GetComponent<ZombieSight>();
                _zombieSight.OnPlayerGetOutSight += PlayerMiss;
            }
        }

        private void Update()
        {
            _zombieManager.transform.position += 
                (_zombieManager.Target.gameObject.transform.position - _zombieManager.transform.position).normalized 
                * _movingSpeed * Time.deltaTime;
            if(Vector3.Distance(_zombieManager.Target.gameObject.transform.position, _zombieManager.transform.position)
                <= _zombieManager.zombieStatus.AttackRange)
            {
                zombieStateController.ChangeZombieState(ZombieStates.ZombieAttack);
            }
        }

        private void PlayerMiss(Player player)
        {
            FindPlayer();
        }

        private void FindPlayer()
        {
            //Todo: Shake Head To Find Player.
            //Todo: But Finding Player Depends On PlayerSight.
            zombieStateController.ChangeZombieState(ZombieStates.ZombieSearch);
        }
    }
}