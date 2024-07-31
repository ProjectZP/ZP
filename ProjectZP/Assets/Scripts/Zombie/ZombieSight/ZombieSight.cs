namespace ZP.BHS.Zombie
{
    abstract class ZombieSight
    {
        protected ZombieManager ZombieManager;
        protected ZombieStateController ZombieStateController;
        protected ZombieSightStateController ZombieSightStateController;

        public ZombieSight(ZombieSightStateController zombieSightStateController)
        {
            this.ZombieSightStateController = zombieSightStateController;
            this.ZombieManager              = zombieSightStateController.transform.root.GetComponent<ZombieManager>();
            this.ZombieStateController      = zombieSightStateController.transform.root.GetComponent<ZombieStateController>();
        }

        public abstract void OnSightEnter();
        public abstract void OnSightUpdate();
        public abstract void OnSightExit();

    }
}
