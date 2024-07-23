using UnityEditor;
using UnityEngine;

namespace ZP.BHS.Zombie
{
    abstract class ZombieSight
    {
        protected ZombieSightStateController zombieSightStateController;
        protected ZombieManager zombieManager;
        protected ZombieStateController zombieStateController;

        public ZombieSight(ZombieSightStateController zombieSightStateController)
        {
            this.zombieSightStateController = zombieSightStateController;
            zombieManager = zombieSightStateController.GetComponentInParent<ZombieManager>();
            zombieStateController = zombieSightStateController.GetComponentInParent<ZombieStateController>();
        }

        public abstract void OnSightEnter();
        public abstract void OnSightUpdate();
        public abstract void OnSightExit();

    }
}
