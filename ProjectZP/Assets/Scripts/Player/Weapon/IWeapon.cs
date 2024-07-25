namespace ZP.SJH.Weapon
{ 
    public interface IWeapon
    {
        public void DeEquip();
        public void Equip();
        public float GetMinVelocity();

        public float CalculateDamage();

        public bool IsOneHanded();
    }
}