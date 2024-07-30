using UnityEngine;

namespace ZP.SJH.Dummy
{
    class ZombieDeathCounter : MonoBehaviour
    {
        public int ZombieKillCount { get; private set; } = 0;

        public delegate void SendToPlayer(int killcount);
        public event SendToPlayer OnZombieKillCountChanged;

        public void AddZombieKillCount()
        {
            ZombieKillCount++;
            OnZombieKillCountChanged(ZombieKillCount);
        }
    }
}