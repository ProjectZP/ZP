using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZP.SJH.Player
{
    public class PlayerStateManager : MonoBehaviour
    {
        public PlayerStatusManager Status => _status;
        public enum PlayerStateType
        {
            Idle,
            Walk,
            Run,
            Attack,
            Dead
        }

        [SerializeField] private PlayerStatusManager _status;
        [SerializeField]  private PlayerStateType _currentState;
        private Dictionary<PlayerStateType, PlayerState> _playerStates = new();

        private void Awake()
        {
            if(_status == null)
                _status = transform.parent.GetComponent<PlayerStatusManager>();
        }

        private void Start()
        {
            InitializePlayerStates();           
            _currentState = PlayerStateType.Idle;
            _playerStates[PlayerStateType.Idle].enabled = true;
        }

        private void InitializePlayerStates()
        {            
            foreach (var stateComponent in GetComponents<PlayerState>())
            {
                _playerStates.Add(stateComponent.StateType, stateComponent);
            }            
        }

        public void SetState(PlayerStateType newState)
        {
            if (_currentState == newState)
                return;

            _playerStates[_currentState].enabled = false;
            _playerStates[newState].enabled = true;

            _currentState = newState;
        }
    }
}