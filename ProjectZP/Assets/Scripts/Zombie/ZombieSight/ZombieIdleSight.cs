using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace ZP.BHS.Zombie
{

    class ZombieIdleSight : MonoBehaviour //Todo: in this class, Using Player as "Player GameObject" but it's not accurate.
    {
        ZombieManager zombieManager
        {
            get
            {
                if (_zombieManager == null)
                    _zombieManager = GetComponentInParent<ZombieManager>();

                return _zombieManager;
            }
        }
        ZombieManager _zombieManager;
        ZombieSightStateController zombieSightStateController;

        SphereCollider SightCollider;


        private void OnEnable()
        {
            if (zombieSightStateController == null) { zombieSightStateController = GetComponent<ZombieSightStateController>(); }
            if (SightCollider == null) { SightCollider = GetComponent<SphereCollider>(); }

        }

        private void Start()
        {
            StartCoroutine(LookingForPrey());
        }
        

        private IEnumerator LookingForPrey()
        {
            while (true)
            {
                if (Physics.OverlapSphere(this.transform.position, zombieManager.zombieStatus.SightRange, 1 << LayerMask.NameToLayer("Stair")).Length > 0)
                {
                    Collider prey = Physics.OverlapSphere(this.transform.position, zombieManager.zombieStatus.SightRange, 1 << LayerMask.NameToLayer("Stair"))[0];

                    if (JudgePreyIsOnSightAngle(prey) && JudgePreyIsNoObstacle(prey))
                    {
                        zombieSightStateController.FoundTarget(prey.transform.root.GetComponentInChildren<Player>());
                        yield break;
                    }
                }
                yield return new WaitForSeconds(0.5f);
            }
        }

        private bool JudgePreyIsOnSightAngle(Collider prey)
        {
            Vector3 targetVector = prey.transform.position - this.transform.position;

            Debug.Log(MathF.Abs(Vector3.Angle(targetVector, this.transform.forward)));

            if (MathF.Abs(Vector3.Angle(targetVector, this.transform.forward)) <= zombieManager.zombieStatus.SightAngle)
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
            Vector3 targetVector = (prey.transform.position - this.transform.position).normalized;

            if (Physics.RaycastAll(
                this.transform.position,
                prey.transform.position,
                Vector3.Distance(this.transform.position, prey.transform.position),
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
