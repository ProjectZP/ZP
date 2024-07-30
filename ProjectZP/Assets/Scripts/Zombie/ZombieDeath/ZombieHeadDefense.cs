using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ZP.BHS.Zombie
{
    public class ZombieHeadDefense : MonoBehaviour 
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
