using UnityEngine;

namespace ZP.BHS.Zombie
{
    class ZombieAttack : ZombieState
    {

        //Todo: Using Animation Event. To Judge weather Attack Finished.
        private void OnEnable()
        {
            DoAttack();
        }

        private void DoAttack()
        {
            Debug.Log("Attack");
            //Todo: RotateZombie Body to Player Loaction.
            //Todo: Do AttackAnimation.
        }

        //This method Listen OnAttackEnd event.
        private void JudgeNextState()
        {
            if (Vector3.Distance(_zombieManager.Target.gameObject.transform.position, _zombieManager.transform.position)
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