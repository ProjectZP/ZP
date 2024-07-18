using System;
using UnityEngine;

namespace ZP.Villin.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public Action<Transform> OnEnterStair;
        private int currentLayer;

        private void Update()
        {
            // 임시로 레이캐스트 테스트.
            if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit newHit, 2f))
            {
                int newLayer = newHit.collider.gameObject.layer;
                if (currentLayer == newLayer)
                {
                    return;
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
