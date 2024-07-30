using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZP.BHS.Zombie
{
    class ZombieDeathCounter : MonoBehaviour
    {
        private ZombieSpawner _zombieSpawner;
        [SerializeField] private List<GameObject> _observeZombie;


        private void Awake()
        {
            _zombieSpawner = GetComponent<ZombieSpawner>();
            _observeZombie = new List<GameObject>();
        }

        private void Start()
        {
            _zombieSpawner.OnZombieSummoned += SubscribeZombie;

            OnZombieKillCountChanged += tempvoid;
        }

        private void SubscribeZombie(List<GameObject> summonedZombie)
        {
            _observeZombie = summonedZombie;

            for(int ix = 0; ix < summonedZombie.Count; ix++)
            {
                summonedZombie[ix].GetComponent<ZombieStateController>().OnZombieDied += AddZombieKillCount;
            }
        }


        public int ZombieKillCount { get; private set; } = 0;

        public delegate void SendToPlayer(int killcount);
        public event SendToPlayer OnZombieKillCountChanged;

        public void AddZombieKillCount()
        {
            ZombieKillCount++;
            OnZombieKillCountChanged(ZombieKillCount);
        }

        private void tempvoid(int i) //Todo: Delete
        {
            Debug.Log(ZombieKillCount);
        }
    }
}
