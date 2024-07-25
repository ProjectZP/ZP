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

        public ZombieState currentZombieStateAction { get; private set; }
        public ZombieSightStateController zombieSightStateController { get; private set; }
        public ZombieManager zombieManager { get; private set; }
        public NavMeshAgent zombieAgent { get; private set; }
        public Animator zombieAnimator { get; private set; }

        public Rigidbody[] RagdollRigidbody;
        public Collider[] RagdollCollider;
        public CharacterJoint[] characterJoints;

        public ZombieStates currentZombieState { get; private set; }
        private Dictionary<ZombieStates, ZombieState> zombieStateDictionary;

        private void Awake()
        {
            zombieAgent = GetComponent<NavMeshAgent>();
            zombieAnimator = GetComponent<Animator>();
            zombieSightStateController = GetComponentInChildren<ZombieSightStateController>();
            zombieManager = GetComponent<ZombieManager>();
            InitZombieStateDictionary();

            RagdollRigidbody = GetComponentsInChildren<Rigidbody>();
            RagdollCollider = GetComponentsInChildren<Collider>();

            characterJoints = GetComponentsInChildren<CharacterJoint>();


            Debug.Log(characterJoints.Length);
            for(int ix = 0; ix < characterJoints.Length; ix++)
            {
                characterJoints[ix].enableProjection = true;
            }

            for (int ix = 0; ix < RagdollRigidbody.Length; ix++)
            {
                RagdollRigidbody[ix].mass = 7f;
                RagdollRigidbody[ix].isKinematic = true;
            }
        }



        private void OnEnable()
        {
            currentZombieStateAction = zombieStateDictionary[ZombieStates.ZombieIdle];
        }

        private void Update()
        {
            currentZombieStateAction.OnStateUpdate();
            if (zombiedie) { ChangeZombieState(ZombieStates.ZombieDead); }
        }


        public bool zombiedie; //Todo:
        public void ChangeZombieState(ZombieStates changingState)
        {
            if (currentZombieState == changingState || currentZombieState == ZombieStates.ZombieDead)
            {
                return;
            }
            Debug.Log($"{changingState}");



            currentZombieStateAction.OnStateExit();
            currentZombieState = changingState;
            OnZombieStateChanged(changingState);
            zombieAnimator.SetInteger("ZombieState", (int)changingState);
            currentZombieStateAction = zombieStateDictionary[changingState];
            currentZombieStateAction.OnStateEnter();
        }

        //To Add States, This Manages.
        private void InitZombieStateDictionary()
        {
            zombieStateDictionary = new Dictionary<ZombieStates, ZombieState>
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
