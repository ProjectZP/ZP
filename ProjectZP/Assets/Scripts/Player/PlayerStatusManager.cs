using UnityEngine;

namespace ZP.SJH.Player
{ 
    public class PlayerStatusManager : MonoBehaviour
    {
        [HideInInspector] public float MaxStamina;
        [HideInInspector] public float CurrentStamina;
        [HideInInspector] public float StaminaRegen;
        [HideInInspector] public float Hp;
        [HideInInspector] public float WalkSpeed;
        [HideInInspector] public float RunSpeed;

        [SerializeField] private WeaponData _data;

        private void Awake()
        {
            if(_data == null)
                _data = Resources.Load("Data/PlayerData") as WeaponData;
        }

        public void LoadPlayerData()
        {
            if (_data != null)
                LoadPlayerData(_data);
        }

        public void LoadPlayerData(WeaponData data)
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
