using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem.UI;

namespace ZP.BHS.Zombie
{
    class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] GameObject SpawnPointHolder;
        [SerializeField] Transform[] SpawnPoint = new Transform[10]; //Todo: SpawnPointLimit
        [SerializeField] Transform Stair;

        [SerializeField] GameObject WalkingZombie;

        [SerializeField] GameObject[] WalkingZombiePool = new GameObject[20];

        public bool spawnPointSettled;

        private void Awake()
        {
            Transform[] tempTransform = SpawnPointHolder.GetComponentsInChildren<Transform>();

            Debug.Log(tempTransform.Length);

            foreach (Transform t in tempTransform)
            {
                int ix = 0;
                if (t.gameObject.layer == 1 << LayerMask.NameToLayer("Floor"))
                {
                    SpawnPoint[ix] = t;
                    ix++;
                }
                if (t.gameObject.layer == 1 << LayerMask.NameToLayer("Stair"))
                {
                    Stair = t;
                }
            }

            InitZombie();
        }

        [SerializeField] int zombiecount;
        [SerializeField] bool summonzombie;
        private void Update()
        {
            if (summonzombie)
            {
                EnableZombie(zombiecount);
                summonzombie = false;
                zombiecount = 0;
            }
        }

        private void InitZombie()
        {
            for (int ix = 0; ix < 20; ix++)
            {
                GameObject tzombie = Instantiate(WalkingZombie);
                WalkingZombiePool[ix] = tzombie;
                WalkingZombiePool[ix].SetActive(false);
            }
        }
        private void EnableZombie(int ZombieCount)
        {
            for (int ix = 0; ix < ZombieCount; ix++)
            {
                int point = Random.Range(0, 10);
                WalkingZombiePool[ix].SetActive(true);
                WalkingZombiePool[ix].transform.position = SpawnPoint[point].position;
            }
        }
        private void DisableZombie(GameObject deadZombie)
        {
            deadZombie.SetActive(false);
        }
    }
}
