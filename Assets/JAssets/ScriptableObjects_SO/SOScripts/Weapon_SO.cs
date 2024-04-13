using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon_SO : ScriptableObject
{
    public float weight;
    public int damage;
    public int durability, fullDurability;

    public Animator animator;
    public string attackAnimString;
    public string specAnimString;

    public float attackCd;
    public float specialCd;
}