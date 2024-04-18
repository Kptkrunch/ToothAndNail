using UnityEngine;

namespace JAssets.Scripts_SC.SOScripts
{
    [CreateAssetMenu(fileName = "New Pickup", menuName = "Pickup")]
    public class Pickup_SO : ScriptableObject
    {
        public Sprite sprite;
        public string gearName;
        public bool isWeapon, isTool;

        public int remainingDurability;
        public int remainingUses;
    }
}