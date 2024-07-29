using UnityEngine;

namespace ZP.BHS.Zombie
{
    class ZombieBlood : MonoBehaviour
    {
        float passedTime;

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
