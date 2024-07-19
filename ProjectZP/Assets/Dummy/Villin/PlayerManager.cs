using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace ZP.Villin.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public Action<Transform> OnEnterStair;
        public Action OnExitStair;
        private int currentLayer;

        private void Update()
        {
            // Temporarty code for test layer check.
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit newHit, 1f))
            {
#if UNITY_EDITOR
                Debug.DrawLine(transform.position, newHit.point, Color.red);
#endif

                int newLayer = newHit.collider.gameObject.layer;
                if (currentLayer == newLayer)
                {
                    return;
                }
                if (currentLayer == 7 && newLayer != 7)
                {
                    OnExitStair?.Invoke();
                }

                currentLayer = newLayer;

                if (currentLayer == 7)
                {
#if UNITY_EDITOR
                    Debug.Log($"Stair Layer Entered!");
#endif
                    OnEnterStair?.Invoke(transform);
#if UNITY_EDITOR
                    Debug.Log(transform);
#endif
                }
            }
        }

    }
}
