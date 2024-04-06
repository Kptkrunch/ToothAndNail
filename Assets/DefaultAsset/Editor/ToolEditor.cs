using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tools))]
public class ToolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Tool tools = (Tool)target;

        EditorGUILayout.BeginVertical("box");

        tools.toolName = EditorGUILayout.TextField("Tool Name", tools.toolName);
        tools.level = EditorGUILayout.IntField("Level", tools.level);

        EditorGUILayout.EndVertical();

        if(GUI.changed)
        {
            EditorUtility.SetDirty(tools);
        }
    }
}