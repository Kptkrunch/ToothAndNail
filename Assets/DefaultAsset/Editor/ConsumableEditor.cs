using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Consumables))]
public class ConsumablesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Consumables consumables = (Consumables)target;

        EditorGUILayout.BeginVertical("box");

        consumables.consumableName = EditorGUILayout.TextField("Consumable Name", consumables.consumableName);
        consumables.healthIncrease = EditorGUILayout.IntField("Health Increase", consumables.healthIncrease);

        EditorGUILayout.EndVertical();

        if(GUI.changed)
        {
            EditorUtility.SetDirty(consumables);
        }
    }
}