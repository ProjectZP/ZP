using System.Collections.Generic;
using UnityEngine;
using ZP.Villin.Teleport;

namespace ZP.BHS.Zombie
{
    class ZombieSpawner : MonoBehaviour //Todo: Remove debugs.
    {
        [SerializeField] private GameObject[] _zombiePrefabs;
        [SerializeField] private DoorsAndWindowsController _doorsAndWindowsController;
        [SerializeField] private RightEndStageDoorController _rightEndStageDoorController;
        [SerializeField] private LeftEndStageDoorController _leftEndStageDoorController;

        private GameObject _zombieSpawner; //spawn point
        private GameObject[] _spawnPoint;
        private List<GameObject> _zombiesDieOnStair;
        private List<GameObject> _summonedZombies;

        [SerializeField] private int _currentFloor; //Todo: delete serialize

        public delegate void ZombieSummoned(List<GameObject> summonedZombie);
        public event ZombieSummoned OnZombieSummoned;

        private const float _goDown = 2.92f;


        private void CheckSerializeComponentIsNull()
        {
#if UNITY_EDITOR
            if (_zombiePrefabs == null) { Debug.LogWarning("Zombie Prefab Null"); }
            if (_doorsAndWindowsController == null) { Debug.LogWarning("doorsAndWindowsController Null"); }
            if (_rightEndStageDoorController == null) { Debug.LogWarning("rightEndStageDoorController Null"); }
            if (_leftEndStageDoorController == null) { Debug.LogWarning("leftEndStageDoorController Null"); }
#endif
        }


        private void Awake()
        {
            _zombieSpawner = this.gameObject;
            if (_doorsAndWindowsController == null) { _doorsAndWindowsController = FindFirstObjectByType<DoorsAndWindowsController>(); }
            _currentFloor = _doorsAndWindowsController.CurrentFloor;

            int spawnpointcounts = _zombieSpawner.GetComponentsInChildren<Transform>().Length - 1;
            _spawnPoint = new GameObject[spawnpointcounts];
            _summonedZombies = new List<GameObject>(20);
            _zombiesDieOnStair = new List<GameObject>(10);

            for (int i = 0; i < spawnpointcounts; i++)
            {
                _spawnPoint[i] = _zombieSpawner.transform.GetChild(i).gameObject;
            }
#if UNITY_EDITOR
            CheckSerializeComponentIsNull();
#endif
        }

        private void Start()
        {
            _rightEndStageDoorController.OnEndStageDoorClosed += WhenDoorClose;
            _leftEndStageDoorController.OnEndStageDoorClosed += WhenDoorClose;

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

        private void WhenDoorClose()
        {
            _currentFloor = _doorsAndWindowsController.CurrentFloor;
            RemoveZombie();
            SummonZombie();
#if UNITY_EDITOR
            Debug.LogWarning("Current Floor: " + _currentFloor);
#endif
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

            for (int ix = 0; ix < _zombiesDieOnStair.Count; ix++)
            {
                _zombiesDieOnStair[ix].transform.position += Vector3.down * _goDown;
            }

            _summonedZombies.Clear();
        }

        /// <summary>
        /// Calculate Summon Weight Using Current Floor.
        /// </summary>
        /// <param name="CurrentFloor">Current Floor</param>
        /// <returns></returns>
        private float _summonWeight(int CurrentFloor)
        {
            float summonWeight = 0;

            summonWeight = ((float)CurrentFloor * 1f) + 10f;

            return summonWeight;
        }

        public void SummonZombie()
        {
            if (_spawnPoint.Length <= 0) { return; }

            SummonWeightOfEachZombieEffectedByCurrentFloor(_currentFloor);

            _totalSummonWeight = _summonWeight(_currentFloor);
            float currentSummonWeight;

            SuddenSummonEvent();

            Debug.Log("Total Weight : " + _totalSummonWeight);

            for (currentSummonWeight = 0; currentSummonWeight < _totalSummonWeight;)
            {
                float summonRandom = UnityEngine.Random.Range(0f, _walkerZombieSummonWeight + _crawlerZombieSummonWeight + _runnerZombieSummonWeight);
                int randomPoint = UnityEngine.Random.Range(0, _spawnPoint.Length);
                GameObject temporarySummonedZombie;

                if (summonRandom < _walkerZombieSummonWeight)
                {
                    currentSummonWeight += 1f;
                    temporarySummonedZombie =
                    Instantiate(_zombiePrefabs[0], _spawnPoint[randomPoint].transform.position, Quaternion.identity);
                }
                else if (summonRandom < _walkerZombieSummonWeight + _crawlerZombieSummonWeight)
                {
                    currentSummonWeight += 1.5f;
                    temporarySummonedZombie =
                    Instantiate(_zombiePrefabs[1], _spawnPoint[randomPoint].transform.position, Quaternion.identity);
                }
                else
                {
                    currentSummonWeight += 2f;
                    temporarySummonedZombie =
                    Instantiate(_zombiePrefabs[2], _spawnPoint[randomPoint].transform.position, Quaternion.identity);
                }
                _summonedZombies.Add(temporarySummonedZombie);
            }
            OnZombieSummoned(_summonedZombies);
        }

        private float _totalSummonWeight;
        private float _walkerZombieSummonWeight;
        private float _crawlerZombieSummonWeight;
        private float _runnerZombieSummonWeight;
        private void SummonWeightOfEachZombieEffectedByCurrentFloor(int currentfloor)
        {
            float fCurrentFloor = (float)currentfloor;

            _walkerZombieSummonWeight = 1f - ((7f / 171f) * (fCurrentFloor - 1f));
            _crawlerZombieSummonWeight = (1f / 57f) * (fCurrentFloor - 1f);
            _runnerZombieSummonWeight = (4f / 171f) * (fCurrentFloor - 1f);
        }

        private void SuddenSummonEvent()
        {
            int suddenValue = UnityEngine.Random.Range(0, 100);
            if (suddenValue >= 99)
            {
                _walkerZombieSummonWeight = 0f;
                _crawlerZombieSummonWeight = 0f;
                _runnerZombieSummonWeight = 1f;
                Debug.Log("All Runner Zombie");
            }
            else if (suddenValue >= 97)
            {
                _walkerZombieSummonWeight = 0f;
                _crawlerZombieSummonWeight = 1f;
                _runnerZombieSummonWeight = 0f;
                Debug.Log("All Crawler Zombie");
            }
            else if (suddenValue >= 94)
            {
                _walkerZombieSummonWeight = 1f;
                _crawlerZombieSummonWeight = 0f;
                _runnerZombieSummonWeight = 0f;
                Debug.Log("All Walker Zombie");
            }
            else { return; }

            _totalSummonWeight = _totalSummonWeight * 1.2f;
        }
    }
}
