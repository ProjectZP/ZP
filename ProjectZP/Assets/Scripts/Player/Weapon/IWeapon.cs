namespace ZP.SJH.Weapon
{ 
    public interface IWeapon
    {
        public float GetMinVelocity();

        public float CalculateDamage();

        public bool IsOneHanded();
    }
}