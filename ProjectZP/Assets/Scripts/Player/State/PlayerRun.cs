using System;
using PlayerStateType = ZP.SJH.Player.PlayerStateManager.PlayerStateType;

namespace ZP.SJH.Player
{
    public class PlayerRun : PlayerState
    {
        private const float RUN_STAMINA = 0.5f;

        protected override void Awake()
        {
            base.Awake();
            _stateType = PlayerStateType.Run;
        }

        private void FixedUpdate()
        {
            if (MIN_STAMINA < _status.CurrentStamina)
            {
                _status.CurrentStamina -= RUN_STAMINA;
                Math.Clamp(_status.CurrentStamina, MIN_STAMINA, _status.MaxStamina);
            }
        }
    }
}