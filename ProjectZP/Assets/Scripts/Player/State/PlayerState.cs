using System;
using UnityEngine;
using PlayerStateType = ZP.SJH.Player.PlayerStateManager.PlayerStateType;

namespace ZP.SJH.Player
{
    [RequireComponent(typeof(PlayerStateManager))]
    public abstract class PlayerState : MonoBehaviour
    {
        public PlayerStateType StateType => _stateType;

        // Used to prevent CurrentStamina from being zero : If not, regenAmount in RegenStamina() will always be zero.
        protected const float MIN_STAMINA = 0.1f;

        [SerializeField] protected PlayerStateManager _stateManager;
        protected PlayerStatusManager _status => _stateManager.Status;

        protected PlayerStateType _stateType;

        protected virtual void Awake()
        {
            if (_stateManager == null)
                _stateManager = GetComponent<PlayerStateManager>();
        }

        protected void RegenStamina()
        {
            // y = 1 - sqrt(1 - x^2)
            if (_status.CurrentStamina < _status.MaxStamina)
            {
                float regenAmount = 1 - MathF.Sqrt(1 - MathF.Pow(_status.CurrentStamina / _status.MaxStamina, 2f));
                _status.CurrentStamina += _status.StaminaRegen * (regenAmount + 0.1f);
                if (_status.CurrentStamina > _status.MaxStamina)
                    _status.CurrentStamina = _status.MaxStamina;
            }
        }
    }
}