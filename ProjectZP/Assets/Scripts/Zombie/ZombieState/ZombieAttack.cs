using System.Collections;
using UnityEngine;
using ZP.SJH.Player;

namespace ZP.BHS.Zombie
{
    class ZombieAttack : ZombieState
    {
        private float _passedTime;
        public ZombieAttack(ZombieStateController zombieStateController) : base(zombieStateController)
        {
        }

        public override void OnStateEnter()
        {
            _agent.isStopped = true;
            _passedTime = 0;
            DoAttack();
        }

        public override void OnStateUpdate()
        {
            _passedTime += Time.deltaTime;
            if (_passedTime > _zombieManager.zombieStatus.AttackSpeed)
            {
                _passedTime = 0;
                JudgeNextState();
            }
        }

        public override void OnStateExit()
        {
            //
        }

        //Todo: Using Animation Event. To Judge weather Attack Finished.

        private void DoAttack()
        {
            Debug.Log("Attack");
            _passedTime = 0;
            if (Vector3.Distance(_zombieManager.Target.transform.position, _zombieManager.refTransform.position) < _zombieManager.zombieStatus.AttackRange)
            {
                _zombieManager.Target.GetComponent<PlayerStatusManager>().Hp -= 10;
            }
            //Todo: RotateZombie Body to Player Loaction.
            //Todo: Do AttackAnimation.
        }

        //This method Listen OnAttackEnd event.
        private void JudgeNextState()
        {
            if (Vector3.Distance(_zombieManager.Target.transform.position, _zombieManager.refTransform.position)
                <= _zombieManager.zombieStatus.AttackRange)
            {
                // _zombieManager.Target.OnPlayerDamaged().Invoke(_zombieManager.zombieStatus.Damage); //Todo:
                DoAttack();
            }
            else
            {
                zombieStateController.ChangeZombieState(ZombieStates.ZombieChase);
            }
        }
    }
}