using UnityEngine;

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

        public ZombieStatus zombieStatus { get; private set; }

        public Player Target = null;
        public Vector3 targetposition;

        private void Awake()
        {
            zombieStateController = GetComponent<ZombieStateController>();
            _zombieDefense = GetComponent<ZombieDefense>();
            _zombieSight = GetComponentInChildren<ZombieSightStateController>();

            zombieStatus = new ZombieStatus(zombieType);
            zombieStateController.ChangeZombieState(ZombieStates.ZombieIdle);
            _zombieSight.OnPlayerGetInSight += SetTarget;
        }

        private void SetTarget(Player target)
        {
            Target = target;
        }
    }
}