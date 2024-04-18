using UnityEngine;

namespace JAssets.Scripts_SC.Items
{
    public class Consumable : Item
    {
        public string itemName;
        public Sprite sprite;
        public Animator animator;

        public virtual void Consume()
        {
            // get pooled object
        }
    }
}