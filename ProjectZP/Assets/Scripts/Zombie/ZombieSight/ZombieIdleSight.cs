using System;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.BHS.Zombie
{

    class ZombieIdleSight : ZombieSight
    {
        private float _checkTime;
        private const float _checkRate = 0.5f; //Check Player in every 0.5f seconds.

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
                    (ZombieSightStateController.transform.position,
                    ZombieManager.ZombieStatus.SightRange,
                    1 << LayerMask.NameToLayer("Player")).Length > 0)
                {
                    Collider prey = Physics.OverlapSphere
                        (ZombieSightStateController.transform.position,
                        ZombieManager.ZombieStatus.SightRange,
                        1 << LayerMask.NameToLayer("Player"))[0];


                    if (JudgePreyIsOnSightAngle(prey) && JudgePreyIsNoObstacle(prey))
                    {
                        ZombieStateController.ChangeZombieState(ZombieStates.ZombieChase);
                        ZombieSightStateController.FoundTarget(prey.transform.root.GetComponentInChildren<PlayerManager>());
                    }
                }
            }
        }
        public override void OnSightExit() { }


        /// <summary>
        /// Caculate Angle Between Zombie's Forward Vector and prey-Zombie Vector, and Return bool.
        /// </summary>
        /// <param name="prey">Collider Which Layer is Player</param>
        /// <returns></returns>
        private bool JudgePreyIsOnSightAngle(Collider prey)
        {
            Vector3 targetVector = prey.transform.position - ZombieSightStateController.transform.position;

            if (MathF.Abs(Vector3.Angle(targetVector, ZombieSightStateController.transform.forward)) <= ZombieManager.ZombieStatus.SightAngle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Find Obstacle Between Zombie and prey, and Return bool.
        /// </summary>
        /// <param name="prey">Collider Which Layer is Player</param>
        /// <returns></returns>
        private bool JudgePreyIsNoObstacle(Collider prey)
        {
            Vector3 targetVector = (prey.transform.position - ZombieSightStateController.transform.position).normalized;

            if (Physics.RaycastAll(
                ZombieSightStateController.transform.position,
                targetVector,
                Vector3.Distance(ZombieSightStateController.transform.position, prey.transform.position),
                (1 << LayerMask.NameToLayer("Wall")) |
                (1 << LayerMask.NameToLayer("Floor")) |
                (1 << LayerMask.NameToLayer("Stair")) |
                (1 << LayerMask.NameToLayer("Door"))
                ).Length > 0)
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
