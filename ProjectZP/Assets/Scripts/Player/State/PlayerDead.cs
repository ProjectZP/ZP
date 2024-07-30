using UnityEngine;
using ZP.SJH.UI;
using PlayerStateType = ZP.SJH.Player.PlayerStateManager.PlayerStateType;

namespace ZP.SJH.Player
{
    public class PlayerDead : PlayerState
    {
        [SerializeField] private GameObject _inputManager;
        [SerializeField] private PlayerUIManager _uiManager;

        protected override void Awake()
        {
            base.Awake();
            if (_inputManager == null)
                _inputManager = transform.root.Find("Input Manager").gameObject;
            if(_uiManager == null)
                _uiManager = FindFirstObjectByType<PlayerUIManager>();
            _stateType = PlayerStateType.Dead;
        }

        private void OnEnable()
        {
            _stateType = PlayerStateType.Dead;
            _inputManager.SetActive(false);
            _uiManager.OnPlayerDead();
            Time.timeScale = 0.02f;
        }
    }
}