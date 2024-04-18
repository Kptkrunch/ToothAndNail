using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC.SOScripts
{
    [CreateAssetMenu(fileName = "New Consumable", menuName = "Consumable")]
    public class Consumable_SO : ScriptableObject
    {
        [ShowInInspector]
        [SerializeField] public Sprite sprite;
        [ShowInInspector]
        public string itemName;
        [ShowInInspector]
        public Animator animator;
    }
}

