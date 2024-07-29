using System.Net.Sockets;
using UnityEngine;

namespace ZP.BHS.Zombie
{
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
