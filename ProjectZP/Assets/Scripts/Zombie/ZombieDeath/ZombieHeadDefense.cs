using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ZP.BHS.Zombie
{
    public class ZombieHeadDefense : MonoBehaviour //Todo:
    {
        ZombieStateController ZombieStateController;
        ZombieManager ZombieManager;

        private void Awake()
        {
            ZombieStateController = GetComponentInParent<ZombieStateController>();
            ZombieManager = GetComponentInParent<ZombieManager>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            StopCoroutine(RigWeightReturnstoZero());
            ZombieManager.HeadIK.weight = 1;
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    Debug.Log("HeadTrigger With Trigger");
        //    ZombieManager.HeadIK.weight = 1;
        //}

        private void OnCollisionExit(Collision collision)
        {
            StartCoroutine(RigWeightReturnstoZero());
        }

        private IEnumerator RigWeightReturnstoZero()
        {
            while (ZombieManager.HeadIK.weight > 0)
            {
                ZombieManager.HeadIK.weight -= Time.deltaTime;
                yield return null;
            }
        }
    }
}
