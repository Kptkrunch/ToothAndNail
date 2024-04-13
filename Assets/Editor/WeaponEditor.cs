#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace JAssets.Scripts_SC.Editor
{
    [CustomEditor(typeof(Weapon_SO))]
    public class WeaponEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var weaponSo = (Weapon_SO)target;

            EditorGUILayout.LabelField("Weight:", EditorStyles.boldLabel);
            weaponSo.weight = EditorGUILayout.FloatField(weaponSo.weight, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Damage:", EditorStyles.boldLabel);
            weaponSo.damage = EditorGUILayout.IntField(weaponSo.damage, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Durability:", EditorStyles.boldLabel);
            weaponSo.durability = EditorGUILayout.IntField(weaponSo.durability, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Full Durability:", EditorStyles.boldLabel);
            weaponSo.fullDurability = EditorGUILayout.IntField(weaponSo.fullDurability, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Attack Animation String:", EditorStyles.boldLabel);
            weaponSo.attackAnimString = EditorGUILayout.TextField(weaponSo.attackAnimString, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Speical Animation String:", EditorStyles.boldLabel);
            weaponSo.specAnimString = EditorGUILayout.TextField(weaponSo.specAnimString, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Attack Cooldown Duration:", EditorStyles.boldLabel);
            weaponSo.attackCd = EditorGUILayout.FloatField(weaponSo.attackCd, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Special Cooldown Duration:", EditorStyles.boldLabel);
            weaponSo.specialCd = EditorGUILayout.FloatField(weaponSo.specialCd, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Animator Controller:", EditorStyles.boldLabel);
            weaponSo.animator = (Animator)EditorGUILayout.ObjectField(weaponSo.animator,
                typeof(RuntimeAnimatorController), false, GUILayout.Height(30));

            EditorGUILayout.Space();

            if (GUI.changed) EditorUtility.SetDirty(weaponSo);
        }
    }
}
#endif