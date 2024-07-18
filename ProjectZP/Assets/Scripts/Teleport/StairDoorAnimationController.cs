using System;
using UnityEngine;

namespace ZP.Villin.Teleport
{
    public class StairDoorAnimationController : MonoBehaviour
    {
        public Action OnStairDoorClosed;
        // 오류 안 보이도록 하는 임시 필드.
        private bool _stairDoorClosed = false;
        private void Update()
        {
        // 오류 안 보이도록 하는 임시 코드.
            if (_stairDoorClosed)
            {
                OnStairDoorClosed?.Invoke();
            }    
            
        }

        // 오류 안 보이도록 하는 임시 메서드.
        public void SetStairDoorClosed()
        {
            _stairDoorClosed = true;
        }
    }
}
