namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This class contains Zombie Data.
    /// </summary>
    public static class ZombieData // Add Attack Range.
    {
        static float Defense; //??
        static float RunSpeed;
        static float WalkSpeed;
        static float CrawlSpeed;
        static float AttackSpeed;
        static ZombieType zombieType;
    }

    public enum ZombieType
    {
        CrawlerZombie,
        WalkerZombie,
        RunnerZombie,
    }
}
