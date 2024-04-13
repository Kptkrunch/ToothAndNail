using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Consumable")]
public class Consumable_SO : ScriptableObject
{
    public string itemName;
    public int primaryValue;
    public int secondaryValue;
    public Animator animator;
}

