using UnityEngine;

namespace JAssets.Scripts_SC.Items
{
    public class Pickup : MonoBehaviour
    {
        public GameObject parent;
        public string itemName;
        public string itemType;
        
        public void PickupItem()
        {
            parent.SetActive(false);
        }
    }
}