using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Consumable")]
public class Consumables : ScriptableObject
{
    public string consumableName;
    public int healthIncrease;
}