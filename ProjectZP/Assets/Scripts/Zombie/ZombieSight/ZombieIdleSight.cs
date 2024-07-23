using System;
using System.Collections;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.BHS.Zombie
{

    class ZombieIdleSight : ZombieSight //Todo: in this class, Using Player as "Player GameObject" but it's not accurate.
    {
        private float _checkTime;
        private const float _checkRate = 0.5f;

        public ZombieIdleSight(ZombieSightStateController zombieSightStateController) : base(zombieSightStateController)
        {
        }
        public override void OnSightEnter() 
        {
            _checkTime = 0;
        }
        public override void OnSightUpdate() 
        { 
            _checkTime += Time.deltaTime;
            if(_checkTime > _checkRate) 
            {
                _checkTime = 0;

                if (Physics.OverlapSphere
                    (zombieSightStateController.transform.position,
                    zombieManager.zombieStatus.SightRange,
                    1 << LayerMask.NameToLayer("Player")).Length > 0)
                {
                    Collider prey = Physics.OverlapSphere
                        (zombieSightStateController.transform.position,
                        zombieManager.zombieStatus.SightRange,
                        1 << LayerMask.NameToLayer("Player"))[0];


                    if (JudgePreyIsOnSightAngle(prey) && JudgePreyIsNoObstacle(prey))
                    {
                        zombieStateController.ChangeZombieState(ZombieStates.ZombieChase);
                        zombieSightStateController.FoundTarget(prey.transform.root.GetComponentInChildren<PlayerManager>());
                    }
                }
            }
        }
        public override void OnSightExit() { }

        private bool JudgePreyIsOnSightAngle(Collider prey)
        {
            Vector3 targetVector = prey.transform.position - zombieSightStateController.transform.position;

            if (MathF.Abs(Vector3.Angle(targetVector, zombieSightStateController.transform.forward)) <= zombieManager.zombieStatus.SightAngle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool JudgePreyIsNoObstacle(Collider prey)
        {
            Vector3 targetVector = (prey.transform.position - zombieSightStateController.transform.position).normalized;

            if (Physics.RaycastAll(
                zombieSightStateController.transform.position,
                targetVector,
                Vector3.Distance(zombieSightStateController.transform.position, prey.transform.position),
                1 << LayerMask.NameToLayer("Wall")).Length > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
