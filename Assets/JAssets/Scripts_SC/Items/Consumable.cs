using UnityEngine;

namespace JAssets.Scripts_SC.Items
{
    public class Consumable : MonoBehaviour
    {
        public string name;
        public int primaryValue;
        public int SecondaryValue;
        public Sprite sprite;
        public Animator animator;

        public void Consume()
        {
            // get pooled object
        }
    }
}