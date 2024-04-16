#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Consumable_SO))]
    public class ConsumableEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var consumableSO = (Consumable_SO)target;

            EditorGUILayout.LabelField("Name:", EditorStyles.boldLabel);
            consumableSO.name = EditorGUILayout.TextField(consumableSO.name, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Primary Value:", EditorStyles.boldLabel);
            consumableSO.primaryValue = EditorGUILayout.IntField(consumableSO.primaryValue, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Secondary Value:", EditorStyles.boldLabel);
            consumableSO.secondaryValue = EditorGUILayout.IntField(consumableSO.secondaryValue, GUILayout.Height(30));

            EditorGUILayout.Space();

            if (GUI.changed) EditorUtility.SetDirty(consumableSO);
        }
    }
}
#endif