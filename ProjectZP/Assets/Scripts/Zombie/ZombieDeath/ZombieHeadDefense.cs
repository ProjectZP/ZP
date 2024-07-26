using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

        private void OnCollisionEnter(Collision collision) //Todo: Weight Return To it's original value = not wanted.
        {
            Debug.Log("WeaponDetected");
            ZombieManager.HeadIK.weight = 1;
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    Debug.Log("HeadTrigger With Trigger");
        //    ZombieManager.HeadIK.weight = 1;
        //}

        private void OnCollisionExit(Collision collision)
        {
            Debug.Log("WeaponAway");
            ZombieManager.HeadIK.weight = 0;
        }

    }
}
