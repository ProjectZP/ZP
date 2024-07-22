using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This class contains Zombie Data.
    /// </summary>
    public static class ZombieData// Add Attack Range.
    {
        static public float Defense = 10; //??
        static public float RunSpeed = 3;
        static public float WalkSpeed = 1;
        static public float CrawlSpeed = 0.5f;
        static public float AttackSpeed;
        static public float AttackRange = 1.5f;
        static public float SightAngle = 60;
        static public float SightRange = 10;
        static public float ChaseAngle = 60;
        static public float ChaseRange = 15;
        static public float RotationSpeed = 360;


        static ZombieType zombieType;

        //public float Defense { get; private set; }
        //public float RunSpeed { get; private set; }
        //public float WalkSpeed { get; private set; } = 3;
        //public float CrawlSpeed { get; private set; }
        //public float AttackSpeed { get; private set; }
        //public float AttackRange { get; private set; } = 1;
        //public float SightAngle { get; private set; } = 60;
        //public float SightRange { get; private set; } = 5;
        //public float ChaseAngle { get; private set; } = 30;
        //public float ChaseRange { get; private set; } = 10;
    }

    public enum ZombieType
    {
        CrawlerZombie,
        WalkerZombie,
        RunnerZombie,
    }
}
