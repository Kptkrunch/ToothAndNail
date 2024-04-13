using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup", menuName = "Pickup")]
public class Pickup_SO : ScriptableObject
{
    public string gearName;
    public bool isWeapon, isTool;

    public int remainingDurability;
    public int remainingUses;
}