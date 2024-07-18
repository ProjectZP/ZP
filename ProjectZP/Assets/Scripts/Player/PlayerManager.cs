using System;
using UnityEngine;

namespace ZP.SJH.Player
{
    [RequireComponent(typeof(PlayerStatusManager))]
    public class PlayerManager : MonoBehaviour
    {
        public Action<Transform> OnStairEnter;
        public PlayerStatusManager Status => _status;

        private PlayerStatusManager _status;

        private void Awake()
        {
            if(_status == null)
                _status = GetComponent<PlayerStatusManager>();

            _status.LoadPlayerData();
        }
    }
}