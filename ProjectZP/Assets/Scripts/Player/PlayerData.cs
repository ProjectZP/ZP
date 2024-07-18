using UnityEngine;

namespace ZP.SJH.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Create PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public float Stamina;
        public float StaminaRegen;
        public float Hp;
        public float WalkSpeed;
        public float RunSpeed;
    }
}