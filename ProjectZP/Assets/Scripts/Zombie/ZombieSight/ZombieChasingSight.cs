
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// While Zombie Chase Target, it's Sight might have problem when there is Obstacle Like Wall, Out Of Range, Out Of Angle.
    /// </summary>
    class ZombieChasingSight : MonoBehaviour
    {
        ZombieManager zombieManager;
        ZombieSightStateController zombieSightStateController;
        ZombieStateController zombieStateController;

        SphereCollider SightCollider;

        private void OnEnable()
        {
            if (zombieManager == null) { zombieManager = GetComponentInParent<ZombieManager>(); }
            if (zombieSightStateController == null) { zombieSightStateController = GetComponent<ZombieSightStateController>(); }
            if (zombieStateController == null) { zombieStateController = GetComponentInParent<ZombieStateController>(); }
            if (SightCollider == null ) { SightCollider = GetComponent<SphereCollider>(); }
            //Todo:
        }

        private void Update()
        {
            //IsthereAnyProblemWithChaseAngle();
            IsthereAnyProblemWithChaseRange();
            IsthereAnyProblemWithObstacle();
        }

        private void IsthereAnyProblemWithChaseAngle()
        {
            if (Vector3.Angle(this.transform.forward,
                zombieManager.Target.transform.position - this.transform.position)
                > zombieManager.zombieStatus.ChaseAngle)
            {
                Debug.Log("Angle Problem");
                MissTarget();
                return;
            }
        }
        private void IsthereAnyProblemWithChaseRange()
        {
            if (Vector3.Distance(this.transform.position,
                zombieManager.Target.transform.position - this.transform.position)
                > zombieManager.zombieStatus.ChaseRange)
            {
                Debug.Log("Range Problem");
                MissTarget();
                return;
            }
        }
        private void IsthereAnyProblemWithObstacle()
        {
            if (Physics.RaycastAll(
                this.transform.position,
                zombieManager.Target.transform.position,
                Vector3.Distance(this.transform.position, zombieManager.Target.transform.position),
                1 << LayerMask.NameToLayer("Wall")).Length > 0) //"Wall" Found.
            {
                Debug.Log("Obstacle Problem");
                MissTarget();
                return;
            }
        }

        //For Some Reason, Zombie can't Find Target Anymore : MissTarget();
        //So Zombie Go to the Position Where Target Last Seen : ZombieState.ZombieSearch
        //After Arrived that position, Zombie Started To look around : ZombieState.ZombieLookAround
        private void MissTarget()
        {
            zombieManager.targetposition = zombieManager.Target.transform.position;
            zombieStateController.ChangeZombieState(ZombieStates.ZombieSearch); //ZombieState change => that event => sightstate change.
        }
    }
}
