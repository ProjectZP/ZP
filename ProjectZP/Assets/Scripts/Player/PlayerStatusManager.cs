using UnityEngine;

namespace ZP.SJH.Player
{ 
    public class PlayerStatusManager : MonoBehaviour
    {
        public float MaxStamina;
        public float CurrentStamina;
        public float StaminaRegen;
        public float Hp;
        public float WalkSpeed;
        public float RunSpeed;

        [SerializeField] private PlayerData _data;

        private void Awake()
        {
            if(_data == null)
                _data = Resources.Load("Data/PlayerData") as PlayerData;
        }

        public void LoadPlayerData()
        {
            if (_data != null)
                LoadPlayerData(_data);
        }

        public void LoadPlayerData(PlayerData data)
        {
            MaxStamina = data.Stamina;
            CurrentStamina = data.Stamina;
            StaminaRegen = data.StaminaRegen;
            Hp = data.Hp;
            WalkSpeed = data.WalkSpeed;
            RunSpeed = data.RunSpeed;
        }
    }
}
