using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Consumable")]
public class Consumable_SO : ScriptableObject
{
    [ShowInInspector]
    [SerializeField] public Sprite sprite;
    public string itemName;
    public int primaryValue;
    public int secondaryValue;
    [ShowInInspector]
    public Animator animator;
}

