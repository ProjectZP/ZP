using UnityEngine.UIElements;

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
        public float AttackRange { get; private set; } 
        public float SightAngle { get; private set; } 
        public float SightRange { get; private set; } 
        public float ChaseAngle { get; private set; } 
        public float ChaseRange { get; private set; } 
        public float RotationSpeed { get; private set; }

        public ZombieType zombieType { get; private set; }



        //This Method refer to Zombie Data then set this class's Parameters.
        private void LoadZombieData(ZombieType zombieType)
        {
            Defense = ZombieData.Defense;
            RunSpeed = ZombieData.RunSpeed;
            WalkSpeed = ZombieData.WalkSpeed;
            CrawlSpeed = ZombieData.CrawlSpeed;
            AttackSpeed = ZombieData.AttackSpeed;
            AttackRange = ZombieData.AttackRange;
            SightAngle = ZombieData.SightAngle;
            SightRange = ZombieData.SightRange;
            ChaseAngle = ZombieData.ChaseAngle;
            ChaseRange = ZombieData.ChaseRange;
            RotationSpeed = ZombieData.RotationSpeed;

            this.zombieType = zombieType;
        }
    }
}
