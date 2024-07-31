using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZP.SJH.Player;

namespace ZP.SJH.UI
{
    public class PlayerUIManager : MonoBehaviour
    {
        private const float YOU_DIED_SCALE_FACTOR = 0.02f;
        private const float YOU_DIED_MAX_SIZE = 7f;

        [SerializeField] private PlayerStatusManager _statusManager;
        [SerializeField] private Image _hpImage; // 0.0 ~ 0.5
        [SerializeField] private Image _staminaImage; // 0.0 ~ 0.5
        [SerializeField] private TMP_Text _killCountText;
        [SerializeField] private TMP_Text _youDiedText;

        private bool _isPlayerDead = false;

        private void Awake()
        {
            if (_statusManager == null)
                _statusManager = transform.root.GetComponent<PlayerStatusManager>();
            if (_hpImage == null)
                _hpImage = transform.Find("Image_Hp").GetComponent<Image>();
            if (_staminaImage == null)
                _staminaImage = transform.Find("Image_Stamina").GetComponent<Image>();
            if (_killCountText == null)
                _killCountText = transform.Find("Image_KillCount").GetChild(0).GetComponent<TMP_Text>();
            if (_youDiedText == null)
                _youDiedText = GameObject.Find("PlayerDead UI Canvas").transform.GetChild(0).GetComponent<TMP_Text>();
        }

        private void Update()
        {
            if (_isPlayerDead == false)
            {
                UpdateStatusUI();
                UpdateKillCountText();
            }
        }

        private void UpdateStatusUI()
        {
            _hpImage.fillAmount = (_statusManager.Hp / _statusManager.MaxHp) / 2;
            _staminaImage.fillAmount = (_statusManager.CurrentStamina / _statusManager.MaxStamina) / 2;
        }

        private void UpdateKillCountText()
        {
            _killCountText.text = $"Kill\n<size=0.1>{_statusManager.KillCount}</size>";
        }

        private void ShowYouDiedText()
        {
            _youDiedText.gameObject.SetActive(true);
            StartCoroutine(C_ShowYouDiedText());
        }

        private IEnumerator C_ShowYouDiedText()
        {
            while (_youDiedText.fontSize < YOU_DIED_MAX_SIZE)
            {
                _youDiedText.fontSize += YOU_DIED_SCALE_FACTOR;
                yield return null;
            }
        }

        public void OnPlayerDead()
        {
            _isPlayerDead = true;
            ShowYouDiedText();
        }
    }
}