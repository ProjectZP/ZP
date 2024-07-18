using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This Class Manages Zombie's Death.
    /// Zombie Have Core and when Weapon Hit it, Zombie Die.
    /// </summary>
    class ZombieCore : MonoBehaviour
    {
        ZombieManager zombieManager;
        ZombieStateController zombieStateController;
        private void Awake()
        {
            zombieManager = GetComponent<ZombieManager>();
            zombieStateController = GetComponent<ZombieStateController>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
            {
                zombieStateController.ChangeZombieState(ZombieStates.ZombieDead);
            }
        }
    }
}
