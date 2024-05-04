// #if UNITY_EDITOR
// using JAssets.Scripts_SC.SOScripts;
// using UnityEditor;
// using UnityEngine;
//
// namespace Editor
// {
//     [CustomEditor(typeof(Consumable_SO))]
//     public class ConsumableEditor : UnityEditor.Editor
//     {
//         public override void OnInspectorGUI()
//         {
//             var consumableSO = (Consumable_SO)target;
//
//             EditorGUILayout.LabelField("itemName:", EditorStyles.boldLabel);
//             consumableSO.name = EditorGUILayout.TextField(consumableSO.name, GUILayout.Height(30));
//
//             EditorGUILayout.Space();
//
//             if (GUI.changed) EditorUtility.SetDirty(consumableSO);
//         }
//     }
// }
// #endif