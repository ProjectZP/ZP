using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// StateController Manages State Class.
    /// Means, if CurrentState is Idle, disable all other State class on GameObejct.
    /// </summary>
    class ZombieStateController : MonoBehaviour
    {
        public delegate void ZombieStateChanged(ZombieStates zombieState);
        public event ZombieStateChanged OnZombieStateChanged;

        public delegate void ZombieDied();
        public event ZombieDied OnZombieDied;

        public ZombieState currentZombieStateAction { get; private set; }
        public ZombieSightStateController zombieSightStateController { get; private set; }
        public ZombieManager zombieManager { get; private set; }
        public NavMeshAgent zombieAgent { get; private set; }
        public Animator zombieAnimator { get; private set; }
        public ZombieAudioManager zombieAudioManager { get; private set; }
        public ZombieStates currentZombieState { get; private set; }

        public Rigidbody[] RagdollRigidbody;
        public Collider[] RagdollCollider;
        public CharacterJoint[] characterJoints;
        
        private Dictionary<ZombieStates, ZombieState> _zombieStateDictionary;

        private void Awake()
        {
            zombieAnimator      = GetComponent<Animator>();
            zombieAgent         = GetComponent<NavMeshAgent>();
            zombieManager       = GetComponent<ZombieManager>();
            zombieAudioManager  = GetComponent<ZombieAudioManager>();

            zombieSightStateController = GetComponentInChildren<ZombieSightStateController>();
            
            if(_zombieStateDictionary == null)
            {
                InitZombieStateDictionary();
            }

            RagdollRigidbody    = transform.GetChild(0).GetComponentsInChildren<Rigidbody>();
            RagdollCollider     = transform.GetChild(0).GetComponentsInChildren<Collider>();

            characterJoints = GetComponentsInChildren<CharacterJoint>();

            for (int ix = 0; ix < characterJoints.Length; ix++)
            {
                characterJoints[ix].enableProjection = true;
            }

            for (int ix = 0; ix < RagdollRigidbody.Length; ix++)
            {
                RagdollRigidbody[ix].mass = 1f;
                RagdollRigidbody[ix].isKinematic = true;
            }
        }

        private void Start()
        {
            currentZombieStateAction = _zombieStateDictionary[ZombieStates.ZombieIdle];
        }

        private void Update()
        {
            currentZombieStateAction.OnStateUpdate();
        }


        public void ChangeZombieState(ZombieStates changingState)
        {
            if (currentZombieState == changingState || currentZombieState == ZombieStates.ZombieDead)
            {
                return;
            }

            if (changingState == ZombieStates.ZombieDead)
            {
                OnZombieDied();
            }

            currentZombieStateAction.OnStateExit();
            currentZombieState = changingState;
            OnZombieStateChanged(changingState);
            zombieAnimator.SetInteger("ZombieState", (int)changingState);
            currentZombieStateAction = _zombieStateDictionary[changingState];
            currentZombieStateAction.OnStateEnter();
        }

        //To Add States, This Manages.
        private void InitZombieStateDictionary()
        {
            _zombieStateDictionary = new Dictionary<ZombieStates, ZombieState>
            {
                { ZombieStates.ZombieIdle, new ZombieIdle(this) },
                { ZombieStates.ZombiePatrol, new ZombiePatrol(this) },
                { ZombieStates.ZombieChase, new ZombieChase(this) },
                { ZombieStates.ZombieAttack, new ZombieAttack(this)},
                { ZombieStates.ZombieDead, new ZombieDead(this)},
                { ZombieStates.ZombieSearch, new ZombieSearch(this)},
                { ZombieStates.ZombieLookAround, new ZombieLookAround(this)},
            };
        }

    }

    /// <summary>
    /// Zombie's States
    /// </summary>
    enum ZombieStates
    {
        None = -1,
        ZombieIdle,
        ZombiePatrol,
        ZombieAttack,
        ZombieDead,
        ZombieChase,
        ZombieSearch,
        ZombieLookAround,
        Last,
    }
}
