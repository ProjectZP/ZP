using UnityEngine;

namespace ZP.BHS.Zombie
{
    class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] Transform[] SpawnPoint = new Transform[10]; //Todo: SpawnPointLimit
        [SerializeField] bool spawnPointSettled = false;

        private void OnEnable()
        {
            if (spawnPointSettled)
            {
                Transform[] tempTransform = FindObjectsOfType<Transform>();

                foreach (Transform t in tempTransform)
                {
                    int ix = 0;
                    if (t.gameObject.layer == LayerMask.NameToLayer("Alley"))
                    {
                        tempTransform[ix] = t;
                        ix++;
                    }
                    if (t.gameObject.layer == LayerMask.NameToLayer("Stair"))
                    {
                        //t.gameObject.GetComponent<>(); //Todo: FindStair and Subscribe DoorClose.
                    }
                }
            }
        }

    }
}
