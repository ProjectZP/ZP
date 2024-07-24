using System.Collections;
using UnityEngine;

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
            if( _passedTime > _zombieManager.zombieStatus.AttackSpeed ) 
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
            //Todo: RotateZombie Body to Player Loaction.
            //Todo: Do AttackAnimation.
        }

        //This method Listen OnAttackEnd event.
        private void JudgeNextState()
        {
            if (Vector3.Distance(_zombieManager.Target.transform.position, _zombieManager.transform.position)
                <= _zombieManager.zombieStatus.AttackRange)
            {
                DoAttack();
            }
            else
            {
                zombieStateController.ChangeZombieState(ZombieStates.ZombieChase);
            }
        }
    }
}