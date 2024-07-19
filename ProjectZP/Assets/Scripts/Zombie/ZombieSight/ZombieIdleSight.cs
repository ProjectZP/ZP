using System;
using UnityEngine;

namespace ZP.BHS.Zombie
{

    class ZombieIdleSight : MonoBehaviour //Todo: in this class, Using Player as "Player GameObject" but it's not accurate.
    {
        ZombieManager zombieManager;
        ZombieSightStateController zombieSightStateController;

        private void OnEnable()
        {
            if (zombieManager == null) { zombieManager = GetComponentInParent<ZombieManager>(); }
            if (zombieSightStateController == null) { zombieSightStateController = GetComponent<ZombieSightStateController>(); }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (zombieManager.Target != null) { return; }

            //Todo: For Test. I Jusok the code below.
            //if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            //{
            //    JudgeOnSight(other);
            //}
            if (other.transform.name == "DummyPlayerBHS")
            {
                JudgeOnSight(other);
            }
        }

        //Judge Target is on Sight Range.
        private void JudgeOnSight(Collider other)
        {
            Vector3 targetVector = other.transform.position - this.transform.position;

            Debug.Log(MathF.Abs(Vector3.Angle(targetVector, this.transform.forward)));
            Debug.Log(zombieManager.zombieStatus.SightAngle);

            if (MathF.Abs(Vector3.Angle(targetVector, this.transform.forward)) <= zombieManager.zombieStatus.SightAngle)
            {
                JudgeObstacle(other);
            }
            else
            {
                return;
            }
        }

        //Judge Obstacle between Zombie and Target.
        private void JudgeObstacle(Collider other)
        {
            Vector3 targetVector = (other.transform.position - this.transform.position).normalized;


            if (Physics.RaycastAll(
                this.transform.position,
                other.transform.position,
                Vector3.Distance(this.transform.position, other.transform.position),
                LayerMask.NameToLayer("Wall")).Length > 0) //"Wall" Found.
            {
                return;
            }
            else
            {
                zombieSightStateController.FoundTarget(other.transform.root.GetComponentInChildren<Player>());
            }
        }
    }
}
