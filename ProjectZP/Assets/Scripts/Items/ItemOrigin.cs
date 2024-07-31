using UnityEngine;
using ZP.SJH.Player;

namespace ZP.BHS.Item
{
    abstract class ItemOrigin : MonoBehaviour
    {

        protected PlayerManager playerManager;

        protected bool OnHand;


    }
    public enum ItemType
    {
        Bandage,
        Drink,
    }
}
