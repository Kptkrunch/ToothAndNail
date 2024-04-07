using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Tool")]
public class Tool_SO : ScriptableObject
{
    public float weight;
    public int uses, fullUses;
    public string useToolString;
    public float toolCd;
    public RuntimeAnimatorController animator;
}