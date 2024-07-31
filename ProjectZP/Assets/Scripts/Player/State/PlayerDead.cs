using UnityEngine;
using ZP.SJH.UI;
using PlayerStateType = ZP.SJH.Player.PlayerStateManager.PlayerStateType;

namespace ZP.SJH.Player
{
    public class PlayerDead : PlayerState
    {
        [SerializeField] private GameObject _inputManager;
        [SerializeField] private PlayerUIManager _uiManager;
        [SerializeField] private EffectController _effectController;

        protected override void Awake()
        {
            base.Awake();
            if (_inputManager == null)
                _inputManager = transform.root.Find("Input Manager").gameObject;
            if(_uiManager == null)
                _uiManager = FindFirstObjectByType<PlayerUIManager>();
            if(_effectController == null)
                _effectController = transform.root.Find("Effect Controller").GetComponent<EffectController>();
            _stateType = PlayerStateType.Dead;
        }

        private void OnEnable()
        {
            _stateType = PlayerStateType.Dead;
            _inputManager.SetActive(false);
            _uiManager.OnPlayerDead();
            _effectController.SetGrayScaleEffect();
            _effectController.SetGrainEffect();
            Time.timeScale = 0.02f;
        }
    }
}