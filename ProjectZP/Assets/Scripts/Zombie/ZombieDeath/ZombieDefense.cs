using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This class Judge Weapon's Sharpness And Player's Attack Speed.
    /// And Make Weapon Stab on Zombie.
    /// </summary>
    class ZombieDefense : MonoBehaviour
    {
        ZombieManager zombieManager;
        private void Awake()
        {
            zombieManager = GetComponent<ZombieManager>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
            {
                if (collision.gameObject.GetComponent<Equipment>().owner.HandSpeed 
                    * collision.gameObject.GetComponent<Equipment>().Sharpness
                    >= zombieManager.zombieStatus.Defense)
                {
                    //Todo: Weapon Stabbed On Zombie's Body.
                }
            }
        }
    }
}
