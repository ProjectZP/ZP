using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// StateController Manages State Class.
    /// Means, if CurrentState is Idle, disable all other State class on GameObejct.
    /// </summary>
    class ZombieStateController : MonoBehaviour
    {
        public delegate void ZombieStateChanged(ZombieStates zombieState);
        public event ZombieStateChanged OnZombieStateChanged;

        private ZombieState[] _zombieState;
        private ZombieStates previousZombieState = ZombieStates.None;
        private ZombieStates currentZombieState = ZombieStates.None;

        private void Awake()
        {
            OnZombieStateChanged += ChangeZombieState;

            _zombieState = new ZombieState[(int)ZombieStates.Last];
            for (int ix = 0; ix < _zombieState.Length; ++ix)
            {
                string ComponentName = $"Zombie{(ZombieStates)ix}";
                _zombieState[ix] = (ZombieState)GetComponent(ComponentName);
                _zombieState[ix].enabled = false;
            }

            ChangeZombieState(ZombieStates.ZombieIdle);
        }

        //This method runs when ZombieStateChanged.
        //Also Set Zombie To use appropriate State.
        //Each State Control Zombie, Like Move or Animation, yes Unity.
        public void ChangeZombieState(ZombieStates changingState)
        {
            if (currentZombieState == changingState || currentZombieState == ZombieStates.ZombieDead)
            {
                return;
            }
            previousZombieState = currentZombieState;
            currentZombieState = changingState;

            if (previousZombieState != ZombieStates.None) { _zombieState[(int)previousZombieState].enabled = false; }
            if (currentZombieState != ZombieStates.None) { _zombieState[(int)currentZombieState].enabled = true; }
        }
    }

    enum ZombieStates
    {
        None = -1,
        ZombieIdle,
        ZombiePatrol,
        ZombieAttack,
        ZombieDead,
        ZombieChase,
        ZombieSearch,

        Last,

        ZombieWalk,
        ZombieRun,
        ZombieCrawl,
    }
}
