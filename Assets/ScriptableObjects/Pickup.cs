using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup", menuName = "Pickup")]
public class Pickups : ScriptableObject
{
    public string pickupName;
    public int value;
}