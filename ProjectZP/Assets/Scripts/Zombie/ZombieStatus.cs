﻿using System.Collections.Generic;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This class set Zombie's Status By ZombieData.
    /// </summary>
    public class ZombieStatus
    {
        public ZombieStatus(ZombieType zombieType)
        {
            LoadZombieData(zombieType);
        }

        public float Defense { get; private set; }
        public float WalkSpeed { get; private set; }
        public float RunSpeed { get; private set; }
        public float AttackSpeed { get; private set; }
        public float AttackRange { get; private set; }
        public float SightAngle { get; private set; }
        public float SightRange { get; private set; }
        public float ChaseAngle { get; private set; }
        public float ChaseRange { get; private set; }
        public float RotationSpeed { get; private set; }
        public float ZombieDamage { get; private set; }
        public ZombieType zombieType { get; private set; }

        

        //This Method refer to Zombie Data then set this class's Parameters.
        private void LoadZombieData(ZombieType zombieType)
        {
            this.zombieType = zombieType;

            Dictionary<string, string> data = ZombieData.CallDataByType(zombieType);

            Defense         = float.Parse(data["Defense"]);
            WalkSpeed       = float.Parse(data["WalkSpeed"]);
            RunSpeed        = float.Parse(data["RunSpeed"]);
            AttackSpeed     = float.Parse(data["AttackSpeed"]);
            AttackRange     = float.Parse(data["AttackRange"]);
            SightAngle      = float.Parse(data["SightAngle"]);
            SightRange      = float.Parse(data["SightRange"]);
            ChaseAngle      = float.Parse(data["ChaseAngle"]);
            ChaseRange      = float.Parse(data["ChaseRange"]);
            RotationSpeed   = float.Parse(data["RotationSpeed"]);
            ZombieDamage    = float.Parse(data["ZombieDamage"]);
        }
    }
}
