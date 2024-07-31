using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// While Zombie Chase Target, It's Sight may have problem when there is Obstacle Like Wall, Or Out Of Range.
    /// </summary>
    class ZombieChaseSight : ZombieSight
    {
        public ZombieChaseSight(ZombieSightStateController zombieSightStateController) : base(zombieSightStateController)
        {
        }
        public override void OnSightEnter() { }
        public override void OnSightUpdate() 
        {
            IsthereAnyProblemWithChaseRange();
            IsthereAnyProblemWithObstacle();
        }
        public override void OnSightExit() { }


        /// <summary>
        /// Check Distance Between Target and This(Zombie).
        /// </summary>
        private void IsthereAnyProblemWithChaseRange()
        {
            if (Vector3.Distance(ZombieManager.transform.position,
                ZombieManager.Target.transform.position)
                > ZombieManager.ZombieStatus.ChaseRange)
            {
                Debug.Log("RangeProblem");
                MissTarget();
                return;
            }
        }

        /// <summary>
        /// Check Obstacle Between Target and This(Zombie).
        /// </summary>
        private void IsthereAnyProblemWithObstacle()
        {
            if (Physics.RaycastAll(
                ZombieManager.transform.position,
                (ZombieManager.Target.transform.position - ZombieManager.transform.position).normalized,
                Vector3.Distance(ZombieManager.transform.position, ZombieManager.Target.transform.position),
                (1 << LayerMask.NameToLayer("Wall")) |
                (1 << LayerMask.NameToLayer("Floor")) |
                (1 << LayerMask.NameToLayer("Stair")) |
                (1 << LayerMask.NameToLayer("Door"))
                ).Length > 0)
            {
                Debug.Log("ObstacleProblem");
                MissTarget();
                return;
            }
        }

        /// <summary>
        /// When this method runs, Change Zombie's State to Search.
        /// </summary>
        private void MissTarget()
        {
            Debug.Log("Miss Target");
            ZombieManager.targetposition = ZombieManager.Target.transform.position;
            ZombieStateController.ChangeZombieState(ZombieStates.ZombieSearch);
        }
    }
}
