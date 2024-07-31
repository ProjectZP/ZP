using UnityEngine;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This class Make Zombie Head Look Forward.
    /// </summary>
    class ZombieHeadLocPos : MonoBehaviour
    {
        [SerializeField] GameObject Neck;

        Vector3 HeadPosition;
        Vector3 OriginRotation = new Vector3(0f, -90f, -90f);

        private void Update()
        {
            this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(OriginRotation), 1f);
        }
    }
}
