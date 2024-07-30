using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

namespace ZP.BHS.Zombie
{
    class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _zombieSpawner; //spawn point
        [SerializeField] private GameObject[] _zombiePrefabs;
        private GameObject[] _spawnPoint;
        private List<GameObject> _summonedZombies;
        private GameObject _doorEventManager; //Observe Target



        private void Awake()
        {
            int spawnpointcounts = _zombieSpawner.GetComponentsInChildren<Transform>().Length - 1;
            _spawnPoint = new GameObject[spawnpointcounts];
            _summonedZombies = new List<GameObject>(20);

            for (int i = 0; i < spawnpointcounts; i++)
            {
                _spawnPoint[i] = _zombieSpawner.transform.GetChild(i).gameObject;
            }

            // _doorEventManager.GetComponent<DoorEvent>().OnDoorClose += WhenDoorClose; //Todo: Connect to Door.
        }

        public bool ssus;
        private void Update()
        {
            if (ssus)
            {
                ssus = false;
                RemoveZombie();
                SummonZombie();
            }
        }

        public void WhenDoorClose()
        {
            RemoveZombie();
            SummonZombie();
        }

        public void RemoveZombie()
        {
            if (_summonedZombies.Count <= 0 || _summonedZombies == null) { return; }

            int summonedCount = _summonedZombies.Count;

            for (int i = 0; i < summonedCount; i++)
            {
                Destroy(_summonedZombies[i]);
            }

            _summonedZombies.Clear();
        }

        public void SummonZombie()
        {
            if (_spawnPoint.Length <= 0) { return; }

            int spawncount = UnityEngine.Random.Range(1, 10); //Todo: Select Spawn Count.

            for (int i = 0; i < spawncount; i++)
            {
                int randomPoint = UnityEngine.Random.Range(0, _spawnPoint.Length); //Todo: Pick Random SpawnPoint.
                int randomZobmie = UnityEngine.Random.Range(0, _zombiePrefabs.Length); //Todo: Pick Random Zombie.

                //Instantiate Random Zombie At Random Point.
                GameObject temporarySummonedZombie =
                Instantiate(_zombiePrefabs[randomZobmie], _spawnPoint[randomPoint].transform.position, Quaternion.identity);

                _summonedZombies.Add(temporarySummonedZombie);
            }

            Debug.Log(_summonedZombies.Count);
        }
    }
}
