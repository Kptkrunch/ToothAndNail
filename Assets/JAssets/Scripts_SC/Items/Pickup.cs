using UnityEngine;

namespace JAssets.Scripts_SC.Items
{
    public class Pickup : MonoBehaviour
    {
        public GameObject parent;
        public string gearName;
        public bool isWeapon, isTool;

        public int remainingDurability;
        public int remainingUses;

        public void PickupGear()
        {
            parent.SetActive(false);
        }
    }
}