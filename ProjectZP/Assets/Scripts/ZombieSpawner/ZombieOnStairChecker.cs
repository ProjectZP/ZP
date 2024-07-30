using System.Collections.Generic;
using UnityEngine;

namespace ZP.BHS.Zombie
{
    class ZombieOnStairChecker : MonoBehaviour
    {
        private ZombieSpawner _zombieSpawner;
        [SerializeField] private List<GameObject> _observeZombie;

        public bool IsLivingZombieOnStair { get; private set; }

        private int _onStairZombieCount;

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

            for (int ix = 0; ix < summonedZombie.Count; ix++)
            {
                summonedZombie[ix].GetComponent<ZombieManager>().OnZombieLocationChanged += CountStairZomibeAtStair;
            }
        }

        [SerializeField] bool stairChecker;
        private void Update()
        {
            if(stairChecker)
            {
                stairChecker = false;
                Debug.Log(_onStairZombieCount);
            }
        }

        private void CountStairZomibeAtStair(bool onstair)
        {
            if (onstair) _onStairZombieCount++;
            else _onStairZombieCount--;

            if (_onStairZombieCount == 0)
            {
                IsLivingZombieOnStair = false;
            }
            else if (_onStairZombieCount > 0)
            {
                if (!IsLivingZombieOnStair)
                { IsLivingZombieOnStair = true; }
            }
            else
            {
                Debug.Log("On Stair Zombie Count Is Minus");
            }
        }
    }
}
