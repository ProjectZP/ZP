using UnityEngine;

namespace ZP.BHS.Zombie
{
    class ZombieAttack : ZombieState
    {
        private float _passedTime;

        private bool _onAttack = false;

        private bool _tempbool = false; //Todo: delete

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
            if (_onAttack && _passedTime > 0.2f)
            {
                _onAttack = false;
                if (Vector3.Distance(_zombieManager.Target.transform.position, _zombieManager.refTransform.position) < _zombieManager.zombieStatus.AttackRange)
                {
                    if (!_tempbool) //Todo: delete
                    {
                        _tempbool = true;
                        _zombieManager.Target.OnPlayerDamaged += tempvoid;
                    } 
                    _zombieManager.Target.OnPlayerDamaged(_zombieManager.zombieStatus.ZombieDamage); //Todo: Damage To Player
                }
            }

            if (_passedTime > _zombieManager.zombieStatus.AttackSpeed)
            {
                _passedTime = 0;
                JudgeNextState();
            }
        }

        public override void OnStateExit()
        {

        }

        private void DoAttack()
        {
            _passedTime = 0;
            _onAttack = true;
        }

        //This method Listen OnAttackEnd event.
        private void JudgeNextState()
        {
            if (Vector3.Distance(_zombieManager.Target.transform.position, _zombieManager.refTransform.position)
                <= _zombieManager.zombieStatus.AttackRange)
            {
                DoAttack();
            }
            else
            {
                zombieStateController.ChangeZombieState(ZombieStates.ZombieChase);
            }
        }

        private void tempvoid(float damage) //Todo: Delete
        {
            Debug.Log(damage);
        }
    }
}