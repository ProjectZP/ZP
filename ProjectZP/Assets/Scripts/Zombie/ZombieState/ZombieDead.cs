using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This class controls Zombie's Death.
    /// And Turn off amount of Components.
    /// </summary>
    class ZombieDead : ZombieState
    {
        float _deathTimer = 0;
        float deathTimer
        {
            get { return _deathTimer; }
            set
            {
                _deathTimer = value;
            }
        }

        public ZombieDead(ZombieStateController zombieStateController) : base(zombieStateController)
        {
            _zombieAudioManager.PlayAudioClip(_zombieAudioManager.AudioClipDamage);
        }

        public override void OnStateEnter()
        {
            for (int ix = 0; ix < _zombieStateController.RagdollRigidbody.Length; ix++)
            {
                _zombieStateController.RagdollRigidbody[ix].isKinematic = false;
                _zombieStateController.RagdollRigidbody[ix].velocity = Vector3.zero;
            }

            _agent.isStopped = true;
            _agent.velocity = Vector3.zero;
            _agent.acceleration = 0f;
            _agent.updatePosition = false;
            _agent.updateRotation = false;
            _agent.enabled = false;

            _zombieStateController.zombieAnimator.applyRootMotion = false;
            _zombieStateController.zombieAnimator.enabled = false;


            if (_zombieManager.OnStairRight)
            {
                _zombieManager.OnStairButDead = true;
                _zombieManager.OnStairRight = false;
                Debug.Log("Zombie Died On right Stair");
            }
            if (_zombieManager.OnStairLeft) 
            {
                _zombieManager.OnStairButDead = true;
                _zombieManager.OnStairLeft = false;
                Debug.Log("Zombie Died On left Stair");
            }


            _zombieSightStateController.enabled = false;
        }

        public override void OnStateUpdate()
        {
            deathTimer += Time.deltaTime;
        }

        public override void OnStateExit()
        {
        }
    }
}