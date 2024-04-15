// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
// using System.Reflection;
//
// public class DictionaryEditor : EditorWindow
// {
//     GridData gridData;
//     private bool gridLocked = false;
//     float boxSpacing = 10f;
//     GameObject currentSelectedGameObject = null;
//     string searchField = "";
//     List<GridData> gridDataSearchResults = new List<GridData>();
//     List<GameObject> assetSearchResults = new List<GameObject>();
//     Vector2 scrollViewVector = Vector2.zero;
//
//     [MenuItem("Window/DictionaryEditor")]
//     public static void ShowWindow()
//     {
//         var window = GetWindow<DictionaryEditor>("DictionaryEditor");
//         window.gridData = ScriptableObject.CreateInstance<GridData>();
//     }
//
//     private void OnGUI()
//     {
//         scrollViewVector = GUILayout.BeginScrollView(scrollViewVector, true, true);
//
//         GUILayout.BeginHorizontal(); // HHHHHHHHHHHHHHHHHHHHHHHH start
//         GUILayout.BeginVertical(GUILayout.MaxWidth(150)); // VVVVVVVVVVVVVVVVVVVVVVVVV start
//         gridLocked = EditorGUILayout.Toggle("Grid Locked", gridLocked);
//
//         GUILayout.Label("Grid size: ", GUILayout.ExpandWidth(false));
//         gridData.gridWidth = EditorGUILayout.IntField("Width", gridData.gridWidth);
//         gridData.gridHeight = EditorGUILayout.IntField("Height", gridData.gridHeight);
//         boxSpacing = EditorGUILayout.FloatField("Box Spacing: ", boxSpacing);
//
//         if (GUILayout.Button("Save Data Grid"))
//         {
//             if (AssetDatabase.Contains(gridData))
//             {
//                 EditorUtility.SetDirty(gridData);
//             }
//             else
//             {
//                 string path = EditorUtility.SaveFilePanelInProject("Save grid data", "GridData", "asset", "Please enter a file name to save grid data to");
//                 if (!string.IsNullOrEmpty(path))
//                 {
//                     AssetDatabase.CreateAsset(gridData, path);
//                 }
//             }
//
//             AssetDatabase.SaveAssets();
//             AssetDatabase.Refresh();
//         }
//
//         if (GUILayout.Button("Clear All"))
//         {
//             currentSelectedGameObject = null;
//             gridDataSearchResults.Clear();
//             assetSearchResults.Clear();
//             currentSelectedGameObject = null;
//
//         }
//
//         if (GUILayout.Button("Close and Open New"))
//         {
//             Close();
//             currentSelectedGameObject = null;
//             ShowWindow();
//         }
//
//         if (currentSelectedGameObject != null)
//             DrawObjectDetails(currentSelectedGameObject);
//
//         GUILayout.Label("Search Assets:");
//         GUILayout.BeginHorizontal();  // HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH start
//         searchField = EditorGUILayout.TextField(searchField, GUILayout.ExpandWidth(true));
//
//         if (GUILayout.Button("Search", GUILayout.MaxWidth(60)))
//         {
//             assetSearchResults.Clear();
//             string[] guids = AssetDatabase.FindAssets(searchField, new[] { "Assets" });
//             foreach (string guid in guids)
//             {
//                 string assetPath = AssetDatabase.GUIDToAssetPath(guid);
//                 GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
//                 if (asset != null)
//                     assetSearchResults.Add(asset);
//             }
//         }
//
//         GUILayout.EndHorizontal(); // HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH end
//
//         GUILayout.Label("Search Results:");
//         // scrollViewVector = GUILayout.BeginScrollView(scrollViewVector, GUILayout.MaxHeight(150));
//         foreach (GameObject asset in assetSearchResults)
//         {
//             if (GUILayout.Button(asset.name))
//             {
//                 EditorGUIUtility.PingObject(asset);
//                 Selection.activeObject = asset;
//             }
//         }
//
//         // GUILayout.EndScrollView();
//
//         if (GUILayout.Button("Load Data Grid"))
//         {
//             string path = EditorUtility.OpenFilePanel("Load grid data", "Assets", "asset");
//             if(!string.IsNullOrEmpty(path))
//             {
//                 path = path.Replace(Application.dataPath, "Assets"); // convert to relative path
//                 gridData = AssetDatabase.LoadAssetAtPath<GridData>(path);
//             }
//         }
//
//         if (GUILayout.Button("Find GridData Assets"))
//         {
//             // Logic for finding GridData assets
//             gridDataSearchResults.Clear();
//             var guids = AssetDatabase.FindAssets("t:GridData", new[] {"Assets"});
//             foreach (string guid in guids)
//             {
//                 string assetPath = AssetDatabase.GUIDToAssetPath(guid);
//                 GridData asset = AssetDatabase.LoadAssetAtPath<GridData>(assetPath);
//                 if (asset != null)
//                     gridDataSearchResults.Add(asset);
//             }
//         }
//
//         GUILayout.Label("GridData Assets:");
//         foreach (GridData gridDataObj in gridDataSearchResults)
//         {
//             if (GUILayout.Button(gridDataObj.name))
//                 gridData = gridDataObj;
//         }
//
//         GUILayout.EndVertical();  // VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV  end
//
//         GUILayout.BeginVertical(); // VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV start
//
//         for (int y = 0; y < gridData.gridHeight; y++)
//         {
//             GUILayout.BeginHorizontal(); // HHHHHHHHHHHHHHHHHHHHHHHHHH start
//             
//             for (int x = 0; x < gridData.gridWidth; x++)
//             {
//                 string positionStr = $"({x},{y})";
//                 GameObject go;
//                 gridData.grid.TryGetValue(positionStr, out go);
//                 GUILayout.BeginVertical(GUILayout.Width(64 + boxSpacing)); // VVVVVVVVVVVVVVVVVVVVVVVV start
//                 GUILayout.Label(go?.name, GUILayout.Width(64));
//                 GUIContent content = new GUIContent
//                 {
//                     image = go != null ? go.GetComponent<SpriteRenderer>().sprite.texture : null
//                 };
//
//                 if (!gridLocked)
//                 {
//                     if (GUILayout.Button(content, GUILayout.Width(64), GUILayout.Height(64)))
//                     {
//                         Debug.Log("Button at position " + positionStr + " clicked.");
//                         var selectedObj = Selection.activeObject as GameObject;
//                         if (selectedObj != null)
//                         {
//                             currentSelectedGameObject = selectedObj;
//                             if (!currentSelectedGameObject && !gridLocked) gridData.grid[positionStr] = selectedObj;
//                             Debug.Log("Added new object: " + selectedObj.name);
//                         }
//                     }
//                 
//                     GUILayout.EndVertical();  // VVVVVVVVVVVVVVVVVVVVV end
//                 }
//             }
//
//             
//             GUILayout.EndHorizontal(); // HHHHHHHHHHHHHHHHHHHHHHH end
//         }
//
//         GUILayout.EndVertical(); // VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV end
//         
//         GUILayout.EndHorizontal(); // HHHHHHHHHHHHHHHHHHHHHHHHHHHH end
//         GUILayout.EndScrollView();
//     }
//
//     private void DrawObjectDetails(GameObject gameObject)
//     {
//         Component[] components = gameObject.GetComponents<Component>();
//         GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(150)); 
//
//         foreach (Component component in components)
//         {
//             if (component.GetType() != typeof(Transform))
//             {
//                 GUIStyle myStyle = new GUIStyle(GUI.skin.label);
//                 myStyle.fontStyle = FontStyle.Bold;
//                 myStyle.fontSize = 12;
//                 Color originalColor = GUI.color;
//                 GUI.color = Color.green;  
//                 GUILayout.Label(component.GetType().Name, myStyle);
//                 GUI.color = originalColor;
//                 var fields = component.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
//                 foreach(var field in fields)
//                 {
//                     GUILayout.Label(field.Name + ": " + field.GetValue(component));
//                 }
//                 GUILayout.Space(10);
//             }
//         }        
//         GUILayout.EndVertical();
//     }
// }