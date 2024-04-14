using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Tool")]
public class Tool_SO : ScriptableObject
{
    public Sprite sprite;
    public float weight;
    public int uses, fullUses;
    public string useToolString;
    public float toolCd;
    public Animator animator;
}