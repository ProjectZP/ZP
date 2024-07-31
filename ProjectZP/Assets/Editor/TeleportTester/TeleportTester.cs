using System;
using System.Collections.Generic;
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
        [SerializeField] private RightEndStageDoorController _rightEndStageDoorController;
        [SerializeField] private LeftEndStageDoorController _leftEndStageDoorController;
        [SerializeField] private StartStageDoorController _startStageDoorController;
        [SerializeField] private StartStageDoorController _startLeftStageDoorController;
        [SerializeField] private SingleDoorController _singleDoorController;
        [SerializeField] private Vector3 _stairPosition = new Vector3(30.8899994f, 0.519999981f, 2.45000005f);

        private List<object> _buffer = new List<object>();

        private void OnEnable()
        {
            
        }

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

            if (_rightEndStageDoorController == null)
            {
                _rightEndStageDoorController = FindFirstObjectByType<RightEndStageDoorController>();
            }
            if (_leftEndStageDoorController == null)
            {
                _leftEndStageDoorController = FindFirstObjectByType<LeftEndStageDoorController>();
            }

            if (_playerManager == null)
            {
                _playerManager= FindFirstObjectByType<PlayerManager>();
            }
        }

        private void DrawTeleportStatePanel()
        {
            _stairPosition = EditorGUILayout.Vector3Field("StairPosition", _stairPosition);
            _teleportManager = (EditorGUILayout.ObjectField("TeleportManager", _teleportManager, typeof(TeleportManager), true) as TeleportManager);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("ExcuteRightTeleport", GUILayout.Width(150f), GUILayout.Height(50f)) == true)
            {
                _rightEndStageDoorController.InteractDoor();
            }
            if (GUILayout.Button("ExcuteLeftTeleport", GUILayout.Width(150f), GUILayout.Height(50f)) == true)
            {
                _leftEndStageDoorController.InteractDoor();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("PlayerToStair", GUILayout.Width(150f), GUILayout.Height(50f)) == true)
            {
                if (_teleportManager.GetNowRemainTeleportCount() % 2 == 0)
                {
                    _playerManager.gameObject.transform.position = Vector3.one;
                    _playerManager.gameObject.transform.position = _stairPosition;
                }
                else
                {
                    _playerManager.gameObject.transform.position = Vector3.one;
                    _playerManager.gameObject.transform.position = new Vector3(-30.8899994f, 0.519999981f, 2.45000005f);
                }
            }
            if (GUILayout.Button("PlayerToOne", GUILayout.Width(150f), GUILayout.Height(50f)) == true)
            {
                _playerManager.gameObject.transform.position = Vector3.one;
            }
            GUILayout.EndHorizontal ();

            _startStageDoorController = (EditorGUILayout.ObjectField("StartStageDoorController", _startStageDoorController, typeof(StartStageDoorController), true) as StartStageDoorController);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("OpenRightStartStageDoor", GUILayout.Width(150f), GUILayout.Height(50f)) == true)
            {
                _startStageDoorController.OnInteractDoor?.Invoke();
            }
            GUILayout.EndHorizontal();

            _singleDoorController = (EditorGUILayout.ObjectField("SelectedDoor", _singleDoorController, typeof(SingleDoorController), true) as SingleDoorController);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("OpenDoorSelected", GUILayout.Width(150f), GUILayout.Height(50f)) == true)
            {
                _singleDoorController.InteractDoor();
            }
            GUILayout.EndHorizontal();
        }

    }
#endif
}