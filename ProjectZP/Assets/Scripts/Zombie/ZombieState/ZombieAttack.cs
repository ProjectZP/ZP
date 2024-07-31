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
            _zombieAudioManager.PlayAudioClip(_zombieAudioManager.AudioClipAttack);

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
                if (Vector3.Distance(
                    _zombieManager.Target.transform.position,
                    _zombieManager.RefTransform.position) <
                    _zombieManager.ZombieStatus.AttackRange)
                {
                    if (!_tempbool) //Todo: delete
                    {
                        _tempbool = true;
                        _zombieManager.Target.OnPlayerDamaged += tempvoid; //Todo:
                    }
                    _zombieManager.Target.
                        OnPlayerDamaged(_zombieManager.ZombieStatus.ZombieDamage); //Todo: Damage To Player
                }
            }

            if (_passedTime > _zombieManager.ZombieStatus.AttackSpeed)
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


        private void JudgeNextState()
        {
            if (Vector3.Distance(
                _zombieManager.Target.transform.position,
                _zombieManager.RefTransform.position) <=
                _zombieManager.ZombieStatus.AttackRange)
            {
                DoAttack();
            }
            else
            {
                _zombieStateController.ChangeZombieState(ZombieStates.ZombieChase);
            }
        }

        private void tempvoid(float damage) //Todo: Delete
        {
            Debug.Log(damage);
        }
    }
}