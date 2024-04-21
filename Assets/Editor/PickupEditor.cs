#if UNITY_EDITOR
using JAssets.Scripts_SC.SOScripts;
using UnityEditor;
using UnityEngine;

namespace JAssets.Scripts_SC.Editor
{
    [CustomEditor(typeof(Pickup_SO))]
    public class PickupEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var pickupSo = (Pickup_SO)target;

            EditorGUILayout.LabelField("Gear itemName:", EditorStyles.boldLabel);
            pickupSo.gearName = EditorGUILayout.TextField(pickupSo.gearName, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Is Weapon:", EditorStyles.boldLabel);
            pickupSo.isWeapon = EditorGUILayout.Toggle(pickupSo.isWeapon, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Is Tool:", EditorStyles.boldLabel);
            pickupSo.isTool = EditorGUILayout.Toggle(pickupSo.isTool, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Remaining Durability:", EditorStyles.boldLabel);
            pickupSo.remainingDurability = EditorGUILayout.IntField(pickupSo.remainingDurability, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Remaining Uses:", EditorStyles.boldLabel);
            pickupSo.remainingUses = EditorGUILayout.IntField(pickupSo.remainingUses, GUILayout.Height(30));

            EditorGUILayout.Space();

            if (GUI.changed) EditorUtility.SetDirty(pickupSo);
        }
    }
}
#endif