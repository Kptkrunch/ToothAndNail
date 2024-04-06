using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Tool")]
public class Tool : ScriptableObject
{
    public string toolName;
    public int level;
}