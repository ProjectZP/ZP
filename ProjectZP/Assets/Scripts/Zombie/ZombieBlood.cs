using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// Zombie's Blood Effects Should Destroy In Any Seconds.
    /// </summary>
    class ZombieBlood : MonoBehaviour
    {
        float passedTime = 0;

        private void Update()
        {
            passedTime += Time.deltaTime;
            if (passedTime > 0.5f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
