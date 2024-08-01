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

        [SerializeField] private Transform _chestTransform;

        public Transform RefTransform { get { return _chestTransform; } }

        private ZombieStateController _zombieStateController;

        private ZombieSightStateController _zombieSight;

        private NavMeshAgent _navMeshAgent;

        public ZombieStatus ZombieStatus { get; private set; }

        public Camera Target = null;

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
            ZombieStatus = new ZombieStatus(zombieType);

            _zombieStateController = GetComponent<ZombieStateController>();
            _zombieStateController.ChangeZombieState(ZombieStates.ZombieIdle);

            _navMeshAgent = GetComponent<NavMeshAgent>();

            _zombieSight = GetComponentInChildren<ZombieSightStateController>();
            _zombieSight.OnPlayerGetInSight += SetTarget;

            HeadIK.weight = 0;

            _navMeshAgent.speed             = ZombieStatus.WalkSpeed;
            _navMeshAgent.angularSpeed      = ZombieStatus.RotationSpeed;
            _navMeshAgent.stoppingDistance  = 0.5f;    
        }

        private void Update()
        {
            CheckZombieLocation();
        }

        private void CheckZombieLocation()
        {
            _layerCheckRay = new Ray(this.transform.position, Vector3.down);

            if (Physics.Raycast(_layerCheckRay, out _checkHit, 1f, 
                (1 << LayerMask.NameToLayer("Floor")) | 
                (1 << LayerMask.NameToLayer("Stair"))
                ))
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

        private void SetTarget(Camera target)
        {
            Target = target;
        }
    }
}