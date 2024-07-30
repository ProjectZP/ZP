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

        private void Update()
        {
            RegenStamina();
        }
    }
}
