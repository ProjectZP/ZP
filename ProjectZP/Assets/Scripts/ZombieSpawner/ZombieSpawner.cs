using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

namespace ZP.BHS.Zombie
{
    class ZombieSpawner : MonoBehaviour
    {
        private GameObject _zombieSpawner; //spawn point
        [SerializeField] private GameObject[] _zombiePrefabs;
        private GameObject[] _spawnPoint;
        private List<GameObject> _zombiesDieOnStair; // Todo:
        private List<GameObject> _summonedZombies; // Todo:
        private GameObject _doorEventManager; //Observe Target

        public delegate void ZombieSummoned(List<GameObject> summonedZombie);
        public event ZombieSummoned OnZombieSummoned;


        private void Awake()
        {
            _zombieSpawner = this.gameObject;

            int spawnpointcounts = _zombieSpawner.GetComponentsInChildren<Transform>().Length - 1;
            _spawnPoint = new GameObject[spawnpointcounts];
            _summonedZombies = new List<GameObject>(20);
            _zombiesDieOnStair = new List<GameObject>(10);

            for (int i = 0; i < spawnpointcounts; i++)
            {
                _spawnPoint[i] = _zombieSpawner.transform.GetChild(i).gameObject;
            }
        }

        private void Start()
        {
            // _doorEventManager.GetComponent<DoorEvent>().OnDoorClose += WhenDoorClose; //Todo: Connect to Door.
        }

        public bool ssus; //Todo: Delete
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

            if (_zombiesDieOnStair.Count > 0)
            {
                int onstairdiezombie = _zombiesDieOnStair.Count;
                for (int ix = 0; ix < onstairdiezombie; ix++)
                {
                    Destroy(_zombiesDieOnStair[ix]);
                }
            }
            _zombiesDieOnStair.Clear();

            int summonedCount = _summonedZombies.Count;

            for (int i = 0; i < summonedCount; i++)
            {
                if (_summonedZombies[i].GetComponent<ZombieManager>().OnStairButDead)
                {
                    _zombiesDieOnStair.Add(_summonedZombies[i]); //Todo: Zombie Die On Stair Must Move With Player To Lower Floor.
                }
                else
                {
                    Destroy(_summonedZombies[i]);
                }
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

            OnZombieSummoned(_summonedZombies);
        }
    }
}
