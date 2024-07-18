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
        private ZombieStateController zombieStateController = new ZombieStateController();
        public ZombieStatus zombieStatus { get; private set; }
        private ZombieDefense _zombieDefense;
        private ZombieSight _zombieSight;

        public Player Target; //Todo: If Target exist and turn to null, State Changes into idle.

        // private ZombieState zombieState; //Todo: Zombie State is abstract Class.

        private void Awake()
        {
            zombieStatus = new ZombieStatus(zombieType);
            zombieStateController.ChangeZombieState(ZombieStates.ZombieIdle);

            _zombieDefense = GetComponent<ZombieDefense>();

            _zombieSight = GetComponent<ZombieSight>();
            _zombieSight.OnPlayerGetInSight += SetTarget;
        }

        private void SetTarget(Player target)
        {
            Target = target;
        }
    }
}