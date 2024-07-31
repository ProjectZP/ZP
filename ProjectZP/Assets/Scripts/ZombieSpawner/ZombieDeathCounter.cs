using System.Collections.Generic;
using UnityEngine;

namespace ZP.BHS.Zombie
{
    class ZombieDeathCounter : MonoBehaviour
    {
        public delegate void SendToPlayer(int killcount);
        public event SendToPlayer OnZombieKillCountChanged;

        [SerializeField] private List<GameObject> _observeZombie;
        private ZombieSpawner _zombieSpawner;

        public int ZombieKillCount { get; private set; } = 0;

        

        private void Awake()
        {
            _zombieSpawner = GetComponent<ZombieSpawner>();
            _observeZombie = new List<GameObject>();
        }

        private void Start()
        {
            _zombieSpawner.OnZombieSummoned += SubscribeZombie;
        }

        private void SubscribeZombie(List<GameObject> summonedZombie)
        {
            _observeZombie = summonedZombie;

            for(int ix = 0; ix < summonedZombie.Count; ix++)
            {
                _observeZombie[ix].GetComponent<ZombieStateController>().OnZombieDied += AddZombieKillCount;
            }
        }

        public void AddZombieKillCount()
        {
            ZombieKillCount++;
            OnZombieKillCountChanged(ZombieKillCount);
        }
    }
}
