using UnityEditor;
using UnityEngine;
using ZP.SJH.Player;
using PlayerStateType = ZP.SJH.Player.PlayerStateManager.PlayerStateType;

namespace ZP.SJH.TestModule
{
#if UNITY_EDITOR
    public class TestModule : EditorWindow
    {
        [SerializeField] private PlayerStateManager _playerState;

        private void OnGUI()
        {
            DrawPlayerStatePanel();
            AssignDependencies();
        }

        [MenuItem("CustomTools/TestModule/Run TestModule %t")]
        public static void Run()
        {
            EditorWindow.GetWindow<TestModule>();
        }

        private void AssignDependencies()
        {
            if(_playerState == null)
                _playerState = FindFirstObjectByType<PlayerStateManager>();
        }

        private void DrawPlayerStatePanel()
        {
            _playerState = EditorGUILayout.ObjectField("PlayerStateManager", _playerState, typeof(GameObject), true) as PlayerStateManager;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Idle", GUILayout.Width(50f), GUILayout.Height(50f)) == true)
                OnPlayerStateChanged(PlayerStateType.Idle);
            if (GUILayout.Button("Walk", GUILayout.Width(50f), GUILayout.Height(50f)) == true)
                OnPlayerStateChanged(PlayerStateType.Walk);
            if (GUILayout.Button("Run", GUILayout.Width(50f), GUILayout.Height(50f)) == true)
                OnPlayerStateChanged(PlayerStateType.Run);
            GUILayout.EndHorizontal();
        }

        private void OnPlayerStateChanged(PlayerStateType stateType)
        {
            if(Application.isPlaying == true)
                _playerState.SetState(stateType);
        }
    }
#endif
}