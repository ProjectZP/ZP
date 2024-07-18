using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace ZP.BHS.Zombie
{
    class ZombiePatrol : ZombieState, IZombieFoundPlayer
    {
        private float _movingTime = 0;
        private const float _movingSpeed = 10;
        private ZombieSight _zombieSight;

        private Vector3 _heading = new Vector3(0,0,0);

        private void OnEnable()
        {
            if (_zombieSight == null)
            {
                _zombieSight = GetComponent<ZombieSight>();
                _zombieSight.OnPlayerGetInSight += FoundPlayer;
            }

            int tempHeading = UnityEngine.Random.Range(0,360);
            _heading.x = tempHeading;
            _zombieManager.transform.localRotation = quaternion.Euler(_heading);
        }

        private void Update()
        {
            _zombieManager.transform.position += Vector3.forward * _movingSpeed * Time.deltaTime;
        }



        public void FoundPlayer(Player player)
        {
            zombieStateController.ChangeZombieState(ZombieStates.ZombieChase);
        }
    }
}