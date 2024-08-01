using System.Collections.Generic;
using UnityEngine;
using ZP.Villin.Teleport;

namespace ZP.BHS.Zombie
{
    class ZombieOnStairChecker : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _observeZombie;
        private ZombieSpawner _zombieSpawner;
        private TeleportManager _teleportManager;
        public bool IsLivingZombieOnStair { get; private set; }
        public bool IsLivingZombieOnStairLeft { get; private set; }

        private int _onStairZombieCount;
        private int _onStairZombieCountLeft;

        private void Awake()
        {
            _zombieSpawner = GetComponent<ZombieSpawner>();
            _observeZombie = new List<GameObject>();
            _teleportManager = FindFirstObjectByType<TeleportManager>();
        }

        private void Start()
        {
            _zombieSpawner.OnZombieSummoned += SubscribeZombie;
            _teleportManager.OnLeftTeleport += ResetCount;
            _teleportManager.OnRightTeleport += ResetCount;
        }

        private void SubscribeZombie(List<GameObject> summonedZombie)
        {
            _observeZombie = summonedZombie;

            for (int ix = 0; ix < summonedZombie.Count; ix++)
            {
                _observeZombie[ix].GetComponent<ZombieManager>().OnZombieLocationChanged += CountStairZomibeAtStair;
                _observeZombie[ix].GetComponent<ZombieManager>().OnZombieLocationChangedLeft += CountStairZomibeAtStairLeft;
            }
        }



        [SerializeField] bool stairChecker; //Todo: Delete
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
            if (onstair) 
            { 
                _onStairZombieCount++; 
            }
            else 
            { 
                _onStairZombieCount--; 
            }


            if (_onStairZombieCount == 0)
            {
                IsLivingZombieOnStair = false;
            }
            else if (_onStairZombieCount > 0)
            {
                if (!IsLivingZombieOnStair)
                { 
                    IsLivingZombieOnStair = true; 
                }
            }
            else
            {
                Debug.Log("On Right Stair Zombie Count Is Minus");
            }
        }
        
        private void CountStairZomibeAtStairLeft(bool onstair)
        {
            if (onstair) 
            {
                _onStairZombieCountLeft++; 
            }
            else 
            {
                _onStairZombieCountLeft--; 
            }


            if (_onStairZombieCountLeft == 0)
            {
                IsLivingZombieOnStairLeft = false;
            }
            else if (_onStairZombieCountLeft > 0)
            {
                if (!IsLivingZombieOnStairLeft)
                {
                    IsLivingZombieOnStairLeft = true; 
                }
            }
            else
            {
                Debug.Log("On Left Stair Zombie Count Is Minus");
            }
        }

        private void ResetCount()
        {
            _onStairZombieCount = 0;
            _onStairZombieCountLeft = 0;
        }
    }
}
