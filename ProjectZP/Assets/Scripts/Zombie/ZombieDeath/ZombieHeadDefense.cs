using System.Collections;
using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This class Controls Neck Rig Layer's Weight.
    /// </summary>
    public class ZombieHeadDefense : MonoBehaviour 
    {
        ZombieManager ZombieManager;
        private void Awake()
        {
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
