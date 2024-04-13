using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetsCreationPanel : EditorWindow
{
    public string soName;
    GUILayoutOption[] optionsButton = { GUILayout.Width(200), GUILayout.Height(30) };
    GUILayoutOption[] optionsField = { GUILayout.Width(200), GUILayout.Height(20) };
    
    [MenuItem("Window/Assets Creation Panel")]
    public static void ShowWindow()
    {
        GetWindow<AssetsCreationPanel>("Assets Creation Panel");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New Weapon", optionsButton))
        { 
            CreateNewScriptableObject<Weapon_SO>(soName);
        }
        soName = GUILayout.TextField(soName, optionsField);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New Tool", optionsButton))
        { 
            CreateNewScriptableObject<Tool_SO>(soName);
        }
        soName = GUILayout.TextField(soName, optionsField);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New Consumable", optionsButton))
        {
            CreateNewScriptableObject<Consumable_SO>(soName);
        }
        soName = GUILayout.TextField(soName, optionsField);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New Pickup", optionsButton))
        {
            CreateNewScriptableObject<Pickup_SO>(soName);
        }
        soName = GUILayout.TextField(soName, optionsField);

        EditorGUILayout.EndHorizontal();

    }

    // Create and save a new ScriptableObject, then select it
    private void CreateNewScriptableObject<T>(string name) where T : ScriptableObject
    {
        var asset = CreateInstance<T>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (string.IsNullOrEmpty(path))
            path = "Assets";
        else if (Path.GetExtension(path) != "")
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");

        string assetPath = AssetDatabase.GenerateUniqueAssetPath(path + "/" + name + ".asset");

        AssetDatabase.CreateAsset(asset, assetPath);

        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}