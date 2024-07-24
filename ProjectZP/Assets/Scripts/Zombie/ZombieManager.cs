using UnityEngine;
using ZP.SJH.Player;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This class is Component of Zombie GameObject.
    /// Todo: To Instantiate Zombie Faster, zombieStatus Sould Be Setted on Prefab Phase.
    /// </summary>
    public class ZombieManager : MonoBehaviour
    {
        [SerializeField] private ZombieType zombieType;

        private ZombieEquipmentController zombieEquipmentController = new ZombieEquipmentController();

        private ZombieStateController zombieStateController;
        private ZombieDefense _zombieDefense;
        private ZombieSightStateController _zombieSight;

        public Transform refTransform { get { return this.transform; } }

        public ZombieStatus zombieStatus { get; private set; }

        public PlayerManager Target = null;
        public Vector3 targetposition;

        private void Awake()
        {
            zombieStatus = new ZombieStatus(zombieType);
            zombieStateController = GetComponent<ZombieStateController>();
            _zombieDefense = GetComponent<ZombieDefense>();
            _zombieSight = GetComponentInChildren<ZombieSightStateController>();
            zombieStateController.ChangeZombieState(ZombieStates.ZombieIdle);
            _zombieSight.OnPlayerGetInSight += SetTarget;
        }

        private void SetTarget(PlayerManager target)
        {
            Target = target;
        }
    }
}