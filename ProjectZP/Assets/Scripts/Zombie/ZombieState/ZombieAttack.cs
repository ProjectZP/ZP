using UnityEngine;
using ZP.SJH.Player;

namespace ZP.BHS.Zombie
{
    class ZombieAttack : ZombieState
    {
        private float _passedTime;

        private bool _onAttack = false;

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
                    _zombieManager.Target.transform.root.GetComponentInChildren<PlayerManager>().
                        OnPlayerDamaged(_zombieManager.ZombieStatus.ZombieDamage);
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
    }
}