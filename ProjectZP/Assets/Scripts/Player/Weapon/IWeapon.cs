namespace ZP.SJH.Weapon
{ 
    public interface IWeapon
    {
        public void DeEquip();
        public void Equip(bool isMoving);
        public float GetMinVelocity();

        public float CalculateDamage();

        public bool IsOneHanded();
        public int GetHandCount();
        public void SetConstraint(bool isFreeze);
    }
}