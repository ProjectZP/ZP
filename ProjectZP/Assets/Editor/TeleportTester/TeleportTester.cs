using UnityEditor;
using UnityEngine;
using ZP.Villin.Teleport;
using PlayerManager = ZP.SJH.Player.PlayerManager;

namespace ZP.Villin.TestModule
{
#if UNITY_EDITOR
    public class TeleportTester : EditorWindow
    {
        [SerializeField] private TeleportManager _teleportManager;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private EndStageDoorController _endStageDoorController;
        [SerializeField] private Vector3 _stairPosition = new Vector3(45.8899994f, 0.519999981f, 2.45000005f);

        private void OnGUI()
        {
            DrawTeleportStatePanel();
            AssignDependencies();
        }

        [MenuItem("CustomTools/TeleportTester/Run TeleportTesterModule %m")]
        public static void Run()
        {
            EditorWindow.GetWindow<TeleportTester>();
        }

        private void AssignDependencies()
        {
            if (_teleportManager == null)
            {
                _teleportManager = FindFirstObjectByType<TeleportManager>();
            }

            if (_endStageDoorController == null)
            {
                _endStageDoorController = FindFirstObjectByType<EndStageDoorController>();
            }

            if (_playerManager == null)
            {
                _playerManager= FindFirstObjectByType<PlayerManager>();
            }
        }

        private void DrawTeleportStatePanel()
        {
            _teleportManager = EditorGUILayout.ObjectField("TeleportManager", _teleportManager, typeof(GameObject), true) as TeleportManager;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("ExcuteTeleport", GUILayout.Width(150f), GUILayout.Height(50f)) == true)
            {
                _endStageDoorController.ActivateCollision();
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("PlayerToStair", GUILayout.Width(150f), GUILayout.Height(50f)) == true)
            {
                _playerManager.gameObject.transform.position = _stairPosition;
            }
            if (GUILayout.Button("PlayerToOne", GUILayout.Width(150f), GUILayout.Height(50f)) == true)
            {
                _playerManager.gameObject.transform.position = Vector3.one;
            }
            GUILayout.EndHorizontal ();
        }

    }
#endif
}