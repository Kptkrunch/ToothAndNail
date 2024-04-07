#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace JAssets.Scripts_SC.Editor
{
    [CustomEditor(typeof(Tool_SO))]
    public class ToolEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var toolSO = (Tool_SO)target;

            EditorGUILayout.LabelField("Weight:", EditorStyles.boldLabel);
            toolSO.weight = EditorGUILayout.FloatField(toolSO.weight, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Uses:", EditorStyles.boldLabel);
            toolSO.uses = EditorGUILayout.IntField(toolSO.uses, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Full Uses:", EditorStyles.boldLabel);
            toolSO.fullUses = EditorGUILayout.IntField(toolSO.fullUses, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Use Tool String:", EditorStyles.boldLabel);
            toolSO.useToolString = EditorGUILayout.TextField(toolSO.useToolString, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Tool Cooldown Duration:", EditorStyles.boldLabel);
            toolSO.toolCd = EditorGUILayout.FloatField(toolSO.toolCd, GUILayout.Height(30));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Animator Controller:", EditorStyles.boldLabel);
            toolSO.animator = (RuntimeAnimatorController)EditorGUILayout.ObjectField(toolSO.animator,
                typeof(RuntimeAnimatorController), false, GUILayout.Height(30));

            EditorGUILayout.Space();

            if (GUI.changed) EditorUtility.SetDirty(toolSO);
        }
    }
}
#endif