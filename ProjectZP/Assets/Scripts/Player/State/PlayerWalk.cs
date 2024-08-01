using PlayerStateType = ZP.SJH.Player.PlayerStateManager.PlayerStateType;

namespace ZP.SJH.Player
{
    public class PlayerWalk : PlayerState
    {
        protected override void Awake()
        {
            base.Awake();
            _stateType = PlayerStateType.Walk;
        }

        private void OnEnable()
        {
            _stateType = PlayerStateType.Walk;
            _effect.StopRunningEffect();
        }

        private void Update()
        {
            RegenStamina();
        }
    }
}
