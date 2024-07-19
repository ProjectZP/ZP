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
        public float WalkSpeed { get; private set; } = 3;
        public float CrawlSpeed { get; private set; }
        public float AttackSpeed { get; private set; }
        public float AttackRange { get; private set; } = 1;
        public float SightAngle { get; private set; } = 60;
        public float SightRange { get; private set; } = 5;
        public float ChaseAngle { get; private set; } = 30;
        public float ChaseRange { get; private set; } = 10;

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
