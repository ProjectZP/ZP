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

        private bool _onStair = false;
        public bool OnStair
        {
            get
            {
                return _onStair;
            }
            set
            {
                if (_onStair != value) 
                { 
                    _onStair = value;
                    OnZombieLocationChanged(_onStair);
                }
                else { return; }
            }
        }

        private bool _onStairButDead;

        public bool OnStairButDead
        {
            get
            {
                return _onStairButDead;
            }
            set
            {
                _onStairButDead = value;
            }
        }

        public delegate void ZombieLocationChange(bool onstair);
        public event ZombieLocationChange OnZombieLocationChanged;

        private Ray _layerCheckRay;
        private RaycastHit _checkHit;

        private void Awake()
        {
            zombieStatus = new ZombieStatus(zombieType);

            zombieStateController = GetComponent<ZombieStateController>();
            zombieStateController.ChangeZombieState(ZombieStates.ZombieIdle);

            _navMeshAgent = GetComponent<NavMeshAgent>();

            _zombieSight = GetComponentInChildren<ZombieSightStateController>();
            _zombieSight.OnPlayerGetInSight += SetTarget;

            HeadIK.weight = 0;

            _navMeshAgent.speed = zombieStatus.WalkSpeed;
            _navMeshAgent.angularSpeed = zombieStatus.RotationSpeed;
            _navMeshAgent.stoppingDistance = 0.5f;

            
        }

        private void Update()
        {
            CheckZombieLocation();
        }

        private void CheckZombieLocation()
        {
            _layerCheckRay = new Ray(this.transform.position, Vector3.down);

            if (Physics.Raycast(_layerCheckRay, out _checkHit, 1f, (1 << LayerMask.NameToLayer("Floor")) | (1 << LayerMask.NameToLayer("Stair"))))
            {
                GameObject hitGameObject = _checkHit.collider.gameObject;

                if (hitGameObject.layer == LayerMask.NameToLayer("Floor") && !OnStairButDead)
                {
                    OnStair = false;
                }
                else if (hitGameObject.layer == LayerMask.NameToLayer("Stair") && !OnStairButDead)
                {
                    OnStair = true;
                }
                else
                {
                    return;
                }
            }
        }

        private void SetTarget(PlayerManager target)
        {
            Target = target;
        }
    }
}