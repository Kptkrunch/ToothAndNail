using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Pickups))]
public class PickupsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Pickups pickups = (Pickups)target;

        EditorGUILayout.BeginVertical("box");

        pickups.pickupName = EditorGUILayout.TextField("Pickup Name", pickups.pickupName);
        pickups.value = EditorGUILayout.IntField("Value", pickups.value);

        EditorGUILayout.EndVertical();

        if(GUI.changed)
        {
            EditorUtility.SetDirty(pickups);
        }
    }
}