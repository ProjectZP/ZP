using PlayerStateType = ZP.SJH.Player.PlayerStateManager.PlayerStateType;

namespace ZP.SJH.Player
{
    public class PlayerIdle : PlayerState
    {
        protected override void Awake()
        {
            base.Awake();
            _stateType = PlayerStateType.Idle;
        }

        private void OnEnable()
        {
            _stateType = PlayerStateType.Idle;
        }

        private void Update()
        {
            RegenStamina();
        }
    }
}
