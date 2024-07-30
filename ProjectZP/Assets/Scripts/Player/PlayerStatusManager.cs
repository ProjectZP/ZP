using UnityEngine;
using ZP.SJH.Dummy;

namespace ZP.SJH.Player
{
    public class PlayerStatusManager : MonoBehaviour
    {
        public float MaxStamina;
        public float CurrentStamina;
        public float StaminaRegen;
        public float MaxHp;
        public float Hp;
        public float WalkSpeed;
        public float RunSpeed;

        public float MoveSpeed { get; private set; }

        public int KillCount { get; private set; }  

        [SerializeField] private PlayerInputManager _inputManager;
        [SerializeField] private PlayerData _data;
        [SerializeField] private Dummy.ZombieDeathCounter _zombieDeathCounter;
        [SerializeField] private PlayerStateManager _stateManager;


        private void Awake()
        {
            if(_data == null)
                _data = Resources.Load("Data/PlayerData") as PlayerData;
            if (_inputManager == null)
                _inputManager = transform.Find("Input Manager").GetComponent<PlayerInputManager>();
            if (_zombieDeathCounter == null)
                _zombieDeathCounter = FindAnyObjectByType<ZombieDeathCounter>();
            if(_stateManager == null)
                _stateManager = GetComponent<PlayerStateManager>();
        }

        private void Start()
        {
            ChangeMoveSpeed(WalkSpeed);
            //_zombieDeathCounter.OnZombieKillCountChanged += 
            //    (killCount) => KillCount = killCount;
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
            MaxHp = data.MaxHp;
            Hp = data.Hp;
            WalkSpeed = data.WalkSpeed;
            RunSpeed = data.RunSpeed;
        }

        public void ChangeMoveSpeed(float speed)
        {
            _inputManager.ChangeMoveSpeed(speed);
            MoveSpeed = speed;
        }

        public void OnPlayerDamaged(float damage)
        {
            Debug.Log(damage);
            Hp -= damage;
            if(Hp < 0)
            {
                Hp = 0;
                _stateManager.SetState(PlayerStateManager.PlayerStateType.Dead);
            }
        }
    }
}
