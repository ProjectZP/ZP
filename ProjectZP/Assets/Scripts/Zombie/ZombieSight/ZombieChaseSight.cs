
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// While Zombie Chase Target, it's Sight might have problem when there is Obstacle Like Wall, Out Of Range, Out Of Angle.
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



        private void IsthereAnyProblemWithChaseRange()
        {
            if (Vector3.Distance(zombieManager.transform.position,
                zombieManager.Target.transform.position)
                > zombieManager.zombieStatus.ChaseRange)
            {
                Debug.Log("RangeProblem");
                MissTarget();
                return;
            }
        }
        private void IsthereAnyProblemWithObstacle()
        {
            if (Physics.RaycastAll(
                zombieManager.transform.position,
                (zombieManager.Target.transform.position - zombieManager.transform.position).normalized,
                Vector3.Distance(zombieManager.transform.position, zombieManager.Target.transform.position),
                (1 << LayerMask.NameToLayer("Wall")) |
                (1 << LayerMask.NameToLayer("Floor")) |
                (1 << LayerMask.NameToLayer("Stair")) |
                (1 << LayerMask.NameToLayer("Door")) //Todo:
                ).Length > 0)
            {
                Debug.Log("ObstacleProblem");
                MissTarget();
                return;
            }
        }

        //For Some Reason, Zombie can't Find Target Anymore : MissTarget();
        //So Zombie Go to the Position Where Target Last Seen : ZombieState.ZombieSearch
        //After Arrived that position, Zombie Started To look around : ZombieState.ZombieLookAround
        private void MissTarget()
        {
            Debug.Log("Miss Target");
            zombieManager.targetposition = zombieManager.Target.transform.position;
            zombieStateController.ChangeZombieState(ZombieStates.ZombieSearch); //ZombieState change => that event => sightstate change.
        }
    }
}
