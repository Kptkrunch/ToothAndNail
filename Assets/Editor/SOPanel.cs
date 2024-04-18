using System;
using System.Collections.Generic;
using System.IO;
using JAssets.Scripts_SC.SOScripts;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class AssetsCreationPanel : EditorWindow
    {
        public string weaponName;
        public string toolName;
        public string pickupName;
        public string consumableName;
        public string lootTableName;
        public string newFolderName;

        public string itemName = "Item itemName";
        public int itemWeight = 1;

        public string generatedItem = String.Empty;

        private readonly List<LootTable_SO.LootItem> tempItemList = new List<LootTable_SO.LootItem>();

        private static readonly GUILayoutOption[] optionsButton = { GUILayout.Width(200), GUILayout.Height(30) };
        private static readonly GUILayoutOption[] optionsField = { GUILayout.Width(200), GUILayout.Height(20) };

        [MenuItem("Window/Assets Creation Panel")]
        public static void ShowWindow()
        {
            GetWindow<AssetsCreationPanel>("Assets Creation Panel");
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            if (GUILayout.Button("Create Folder", optionsButton))
            {
                CreateFolder();
                ClearInputFields();
            }

            newFolderName = GUILayout.TextField(newFolderName, optionsField);
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Clear All", optionsButton))
            {
                ClearInputFields();
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("New Weapon", optionsButton))
            {
                CreateNewScriptableObject<Weapon_SO>(weaponName);
                ClearInputFields();
            }

            weaponName = GUILayout.TextField(weaponName, optionsField);
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
            if (GUILayout.Button("New Loot Table", optionsButton))
            {
                CreateNewScriptableObject<LootTable_SO>(lootTableName);
                ClearInputFields();
            }

            lootTableName = GUILayout.TextField(lootTableName, optionsField);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical();
            itemName = EditorGUILayout.TextField("LootItem itemName:", itemName);
            itemWeight = EditorGUILayout.IntField("LootItem Weight:", itemWeight);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add LootItem", optionsButton))
            {
                AddItem();
            }

            if (GUILayout.Button("Clear List", optionsButton))
            {
                ClearItemList();
            }

            EditorGUILayout.EndHorizontal();
            if (tempItemList.Count > 0)
            {
                EditorGUILayout.LabelField("Items:");
                for (int i = 0; i < tempItemList.Count; i++)
                {
                    EditorGUILayout.LabelField(
                        $"{i + 1}: itemName: {tempItemList[i].itemName}, Weight: {tempItemList[i].itemWeight}.");
                }
            }

            EditorGUILayout.EndVertical();
            if (GUILayout.Button("Generate Random Item", optionsButton))
            {
                LootTable_SO lootTable = ScriptableObject.CreateInstance<LootTable_SO>();
                lootTable.lootItems = new List<LootTable_SO.LootItem>(tempItemList);
                string lootItem = lootTable.GetRandomLoot();
                generatedItem = lootItem;
            }

            EditorGUILayout.LabelField("Generated Item: " + generatedItem);
        }

        private void AddItem()
        {
            LootTable_SO.LootItem newItem = new LootTable_SO.LootItem { itemName = itemName, itemWeight = itemWeight };
            tempItemList.Add(newItem);
        }

        private void ClearItemList()
        {
            tempItemList.Clear();
        }

        private void ClearInputFields()
        {
            weaponName = string.Empty;
            toolName = string.Empty;
            pickupName = string.Empty;
            consumableName = string.Empty;
            lootTableName = string.Empty;
            itemName = string.Empty;
            itemWeight = 0;
            newFolderName = string.Empty;
            ClearItemList();
        }

        // Create and save a new ScriptableObject, then select it
        private void CreateNewScriptableObject<T>(string soName) where T : ScriptableObject
        {
            var asset = CreateInstance<T>();

            if (asset is LootTable_SO lootTable)
            {
                lootTable.lootItems = new List<LootTable_SO.LootItem>(tempItemList);
                tempItemList.Clear();
            }

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (string.IsNullOrEmpty(path)) path = "Assets";
            else if (Path.GetExtension(path) != "")
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");

            string assetPath = AssetDatabase.GenerateUniqueAssetPath(path + "/" + soName + ".asset");

            AssetDatabase.CreateAsset(asset, assetPath);

            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
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

            // If existing directory, append _N where N is numeric
            var newFolderPath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(path, newFolderName));
            AssetDatabase.CreateFolder(path, newFolderName);
            AssetDatabase.Refresh();
        }
    }
}