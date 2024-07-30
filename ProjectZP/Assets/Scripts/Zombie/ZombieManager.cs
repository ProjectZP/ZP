using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using ZP.SJH.Player;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This class is Component of Zombie GameObject.
    /// </summary>
    public class ZombieManager : MonoBehaviour
    {
        [SerializeField] private ZombieType zombieType;

        private ZombieStateController zombieStateController;
        private ZombieSightStateController _zombieSight;

        private NavMeshAgent _navMeshAgent;

        [SerializeField] private Transform _chestTransform;
        public Transform refTransform { get { return _chestTransform; } }

        public ZombieStatus zombieStatus { get; private set; }

        public PlayerManager Target = null;
        public Vector3 targetposition;

        [SerializeField] public Rig HeadIK;
 
        private void Awake()
        {
            zombieStatus = new ZombieStatus(zombieType);
            zombieStateController = GetComponent<ZombieStateController>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _zombieSight = GetComponentInChildren<ZombieSightStateController>();
            zombieStateController.ChangeZombieState(ZombieStates.ZombieIdle);
            _zombieSight.OnPlayerGetInSight += SetTarget;
            HeadIK.weight = 0;

            _navMeshAgent.speed = zombieStatus.WalkSpeed;
            _navMeshAgent.angularSpeed = zombieStatus.RotationSpeed;
            _navMeshAgent.stoppingDistance = 0.5f;
        }

        private void SetTarget(PlayerManager target)
        {
            Target = target;
        }
    }
}