using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// When Zombie Lost Target, This Enables.
    /// Zombie walk toward the point where target last seen.
    /// when Zombie arrive that point, state changes into LookAround.
    /// </summary>
    class ZombieSearch : ZombieState
    {
        ZombieSightStateController zombieSightStateController;

        private void OnEnable()
        {
            if (_zombieManager == null) { _zombieManager = GetComponentInParent<ZombieManager>(); }
            if (zombieSightStateController == null) { zombieSightStateController = GetComponentInChildren<ZombieSightStateController>(); }
        }

        private void Update()
        {
            _zombieManager.transform.position += 
                _zombieManager.targetposition.normalized * _zombieManager.zombieStatus.WalkSpeed * Time.deltaTime;
            //Todo: ZombieStatus.
        }
    }
}
