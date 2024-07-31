using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace ZP.SJH.Player
{
    public class PlayerInputManager : MonoBehaviour
    {
        private const float IDLE_VALUE = 0.01f;
        private const float RUN_VALUE = 0.98f;

        private const float MIN_STAMINA = 0.1f;
        private const float MIN_RUN_STAMINA = 30f;
        private bool _isTired = false;

        [SerializeField] private PlayerManager _manager;
        [SerializeField] private InputActionProperty _moveAction;
        [SerializeField] private ActionBasedContinuousMoveProvider _moveProvider;
        public float MoveValue => _moveValue;
        private float _moveValue;

        private void Awake()
        {
            if (_manager == null)
                _manager = transform.root.GetComponent<PlayerManager>();
            if (_moveProvider == null)
                _moveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        }

        private void Update()
        {
            _moveValue = _moveAction.action.ReadValue<Vector2>().magnitude;
            if (_moveValue < IDLE_VALUE)
            {
                Idle();
            }
            else if(_moveValue < RUN_VALUE)
            {
                Walk();
            }
            else 
            {
                if (_manager.Status.CurrentStamina > MIN_STAMINA && _isTired == false)
                    Run();
                else
                {
                    Walk();
                    _isTired = true;
                }
            }

            if (_isTired == true && _manager.Status.CurrentStamina >= MIN_RUN_STAMINA)
                _isTired = false;
        }

        public void ChangeMoveSpeed(float speed)
        {
            _moveProvider.moveSpeed = speed;
        }

        private void Idle()
        {
            ChangeMoveSpeed(_manager.Status.WalkSpeed);
            _manager.State.SetState(PlayerStateManager.PlayerStateType.Idle);
        }

        private void Walk()
        {
            ChangeMoveSpeed(_manager.Status.WalkSpeed);
            _manager.State.SetState(PlayerStateManager.PlayerStateType.Walk);
        }

        private void Run()
        {
            ChangeMoveSpeed(_manager.Status.RunSpeed);
            _manager.State.SetState(PlayerStateManager.PlayerStateType.Run);
        }
    }

}