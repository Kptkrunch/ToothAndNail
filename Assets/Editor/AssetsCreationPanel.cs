using System.Collections.Generic;
using System.IO;
using JAssets.Scripts_SC.SOScripts;
using UnityEditor;
using UnityEngine;

public class AssetsCreationPanel : EditorWindow
{
    [SerializeField] private GameObject newRecipe;
    public string weaponName = string.Empty;
    public string toolName = string.Empty;
    public string pickupName = string.Empty;
    public string recipe_soName = string.Empty;
    public string consumableName = string.Empty;
    public string gridName = string.Empty;
    public string itemName = string.Empty;
    public string lootTableName = string.Empty;
    public int itemWeight = 0;
    public string generatedItem = string.Empty;
    public string newFolderName = "New Folder";

    private List<LootTable_SO.LootItem> tempItemList = new List<LootTable_SO.LootItem>();

    private static GUILayoutOption[] optionsButton = { GUILayout.Width(200), GUILayout.Height(30) };
    private static GUILayoutOption[] optionsField = { GUILayout.Width(200), GUILayout.Height(20) };

    [MenuItem("Window/Assets Creation Panel")]
    public static void ShowEditorWindow()
    {
        GetWindow<AssetsCreationPanel>("Assets Creation Panel");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New Weapon", optionsButton))
        {
            CreateNewScriptableObject<Weapon_SO>(weaponName);
            ClearInputFields();
        }
        weaponName = GUILayout.TextField(weaponName,  optionsField);
        EditorGUILayout.EndHorizontal();
        
        toolName = GUILayout.TextField(toolName, optionsField);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New Consumable", optionsButton))
        {
            CreateNewScriptableObject<Consumable_SO>(consumableName);
            ClearInputFields();
        }
        consumableName = GUILayout.TextField(consumableName, optionsField);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New Pickup", optionsButton))
        {
            CreateNewScriptableObject<Pickup_SO>(pickupName);
            ClearInputFields();
        }
        pickupName = GUILayout.TextField(pickupName, optionsField);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New Recipe_SO", optionsButton))
        {
            CreateNewScriptableObject<Recipe_SO>(recipe_soName);
            ClearInputFields();
        }
        recipe_soName = GUILayout.TextField(recipe_soName, optionsField);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New GridData", optionsButton))
        {
            CreateNewScriptableObject<GridData>(gridName);
            ClearInputFields();
        }
        gridName = GUILayout.TextField(gridName, optionsField);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("New Recipe") && newRecipe != null)
        {
            PrefabUtility.InstantiatePrefab(newRecipe);
        }
        newRecipe = (GameObject)EditorGUILayout.ObjectField("Prefab:", newRecipe, typeof(GameObject), false);
        
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New Loot Table", optionsButton))
        {
            CreateNewScriptableObject<LootTable_SO>(lootTableName);
            ClearInputFields();
        }

        lootTableName = GUILayout.TextField(lootTableName, optionsField);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add LootItem", optionsButton))
        {
            AddItem();
        }
        
        EditorGUILayout.BeginVertical();
        itemName = EditorGUILayout.TextField("LootItem itemName:", itemName);
        itemWeight = EditorGUILayout.IntField("LootItem Weight:", itemWeight);
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

        if (tempItemList.Count > 0)
        {
            EditorGUILayout.LabelField("Items:");
            for (int i = 0; i < tempItemList.Count; i++)
            {
                EditorGUILayout.LabelField($"{i + 1}: itemName: {tempItemList[i].itemName}, Weight: {tempItemList[i].itemWeight}.");
            }
        }
        
        if (GUILayout.Button("Generate Random Item", optionsButton))
        {
            LootTable_SO lootTable = CreateInstance<LootTable_SO>();
            lootTable.lootItems = new List<LootTable_SO.LootItem>(tempItemList);
            string lootItem = lootTable.GetRandomLoot();
            generatedItem = lootItem;
        }

        EditorGUILayout.LabelField("Generated Item: " + generatedItem);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Folder", optionsButton))
        {
            CreateFolder();
        }
        newFolderName = GUILayout.TextField(newFolderName, optionsField);
        EditorGUILayout.EndHorizontal();
        
        if (GUILayout.Button("Clear List", optionsButton))
        {
            ClearItemList();
        }
        

        EditorGUILayout.EndVertical();
    }

    private void AddItem()
    {
        LootTable_SO.LootItem newItem = new LootTable_SO.LootItem { itemName = itemName, itemWeight = itemWeight };
        tempItemList.Add(newItem);
    }

    private void ClearItemList()
    {
        tempItemList.Clear();
        ClearInputFields();
    }

    private void ClearInputFields()
    {
        weaponName = string.Empty;
        toolName = string.Empty;
        pickupName = string.Empty;
        consumableName = string.Empty;
        gridName = string.Empty;
        itemName = string.Empty;
        lootTableName = string.Empty;
        itemName = string.Empty;
        itemWeight = 0;
    }

    private void CreateFolder()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (string.IsNullOrEmpty(path))
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }
        
        string newFolderPath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(path, newFolderName));

        AssetDatabase.CreateFolder(path, newFolderPath);
        AssetDatabase.Refresh();
    }

    private void CreateNewScriptableObject<T>(string name) where T : ScriptableObject
    {
        var asset = CreateInstance<T>();
        
        if (asset is LootTable_SO lootTable)
        {
            lootTable.lootItems = new List<LootTable_SO.LootItem>(tempItemList);
            tempItemList.Clear();
        }

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (string.IsNullOrEmpty(path))
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPath = AssetDatabase.GenerateUniqueAssetPath(path + "/" + name + ".asset");

        AssetDatabase.CreateAsset(asset, assetPath);
        
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}