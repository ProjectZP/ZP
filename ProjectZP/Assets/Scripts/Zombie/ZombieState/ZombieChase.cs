using System.Collections;
using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// Zombie Chasing contains Two Phase.
    /// Phase 1. Rotate itself toward target found.
    /// Phase 2. Slowly move toward target. and rotate itself toward Target.
    /// </summary>
    class ZombieChase : ZombieState
    {
        private const float _movingSpeed = 10;
        private ZombieSightStateController _zombieSight;
        private void OnEnable()
        {
            if (_zombieSight == null)
            {
                _zombieSight = GetComponentInChildren<ZombieSightStateController>();
                _zombieSight.OnPlayerGetOutSight += PlayerMiss;
            }


        }

        private void Start()
        {
            StartCoroutine(PlayerJustFound());
        }

        IEnumerator PlayerJustFound()
        {
            while (!_rotateComplete)
            {
                Vector3 targetvector = _zombieManager.Target.transform.position - this.transform.position;
                targetvector.y = 0;

                Quaternion targetRotation = Quaternion.LookRotation(targetvector);
                transform.rotation = Quaternion.RotateTowards
                    (transform.rotation,
                    targetRotation,
                    _zombieManager.zombieStatus.RotationSpeed * Time.deltaTime * (1f/6f));

                if(Vector3.Angle(_zombieManager.Target.transform.position, this.transform.forward) < 10)
                {
                    _rotateComplete = true;
                }
                yield return null;
            }
        }

        private bool _rotateComplete;
        private void Update()
        {
            if (!_rotateComplete) { return; }
            Vector3 targetvector = _zombieManager.Target.transform.position - this.transform.position;
            targetvector.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(targetvector);
            transform.rotation = Quaternion.RotateTowards
                (transform.rotation,
                targetRotation,
                _zombieManager.zombieStatus.RotationSpeed * Time.deltaTime);

            this.transform.position += this.transform.forward * _zombieManager.zombieStatus.WalkSpeed * Time.deltaTime;


            if (Vector3.Distance(_zombieManager.Target.transform.position,
                this.transform.position) <= _zombieManager.zombieStatus.AttackRange)
            { Debug.Log("GoAttack!!"); zombieStateController.ChangeZombieState(ZombieStates.ZombieAttack); }

        }

        private void PlayerMiss(Player player)
        {
            if(Vector3.Distance(_zombieManager.Target.transform.position,
                this.transform.position) <= _zombieManager.zombieStatus.ChaseRange)
            FindPlayer();
        }

        private void FindPlayer()
        {
            //Todo: Shake Head To Find Player.
            //Todo: Finding Player Depends On ZombieSight.
            zombieStateController.ChangeZombieState(ZombieStates.ZombieSearch);
        }
    }
}