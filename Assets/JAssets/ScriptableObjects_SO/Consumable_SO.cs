using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Consumable")]
public class Consumable_SO : ScriptableObject
{
    public string name;
    public int primaryValue;
    public int secondaryValue;
    public RuntimeAnimatorController animator;
}

