using UnityEngine;

namespace JAssets.Scripts_SC.SOScripts
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
    public class Weapon_SO : ScriptableObject
    {
        public Sprite sprite;
        public float weight;
        public int damage;
        public int durability, fullDurability;
        public string attackAnimString;
        public string specAnimString;
        public float attackCd;
        public float specialCd;
    }
}