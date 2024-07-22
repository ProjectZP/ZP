using Unity.Mathematics;
using UnityEngine;

namespace ZP.BHS.Zombie
{
    class ZombiePatrol : ZombieState, IZombieFoundPlayer
    {
        private float _movingTime = 3;
        private float _passedTime = 0;
        private const float _movingSpeed = 3;
        private ZombieSightStateController _zombieSight;

        private Vector3 _heading = new Vector3(0,0,0);

        private void OnEnable()
        {
            if (_zombieSight == null)
            {
                _zombieSight = GetComponentInChildren<ZombieSightStateController>();
                _zombieSight.OnPlayerGetInSight += FoundPlayer;
            }

            int tempHeading = UnityEngine.Random.Range(0,360);
            _heading.y = tempHeading;
            this.transform.localRotation = quaternion.Euler(_heading);
            _passedTime = 0;
        }

        private void Update()
        {
            _passedTime += Time.deltaTime;
            _zombieManager.transform.position += this.transform.forward * _zombieManager.zombieStatus.WalkSpeed * Time.deltaTime;
            if(_passedTime >= _movingTime){ zombieStateController.ChangeZombieState(ZombieStates.ZombieIdle); }
        }



        public void FoundPlayer(Player player)
        {
            zombieStateController.ChangeZombieState(ZombieStates.ZombieChase);
        }
    }
}