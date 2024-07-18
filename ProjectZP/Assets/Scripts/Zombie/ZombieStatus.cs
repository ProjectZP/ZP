namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This class set Zombie's Status By ZombieData.
    /// </summary>
    public class ZombieStatus //Todo: Get Data. Add Attack Range.
    {
        public ZombieStatus(ZombieType zombieType)
        {
            LoadZombieData(zombieType);
        }

        public float Defense { get; private set; }
        public float RunSpeed { get; private set; }
        public float WalkSpeed { get; private set; }
        public float CrawlSpeed { get; private set; }
        public float AttackSpeed { get; private set; }

        public float AttackRange { get; private set; }
        public ZombieType zombieType { get; private set; }



        const int tempStatus = 30;

        //This Method refer to Zombie Data then set this class's Parameters.
        private void LoadZombieData(ZombieType zombieType)
        {
            Defense = tempStatus; 
            RunSpeed = tempStatus; 
            WalkSpeed = tempStatus; 
            CrawlSpeed = tempStatus; 
            AttackSpeed = tempStatus;
            AttackRange = tempStatus;

            this.zombieType = zombieType;
        }
    }
}
