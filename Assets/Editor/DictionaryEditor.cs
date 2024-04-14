using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using UnityEngine.UI;

namespace Editor
{
    public class DictionaryEditor : EditorWindow
    {
        GridData gridData;
        Vector2Int gridSize = new Vector2Int(10, 10);
        Vector2 scrollViewVector = Vector2.zero;
        float boxSpacing = 10f;
        GameObject currentSelectedGameObject = null;
        string searchField = "";
        List<GameObject> assetSearchResults = new List<GameObject>();

        [MenuItem("Window/DictionaryEditor")]
        public static void ShowWindow()
        {
            var window = GetWindow<DictionaryEditor>("DictionaryEditor");
            window.gridData = ScriptableObject.CreateInstance<GridData>();
        }

        private void OnGUI()
        {
            scrollViewVector = GUILayout.BeginScrollView(scrollViewVector, true, true);
            
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical(GUILayout.MaxWidth(150));
            
            GUILayout.Label("Grid size: ", GUILayout.ExpandWidth(false));
            gridSize = EditorGUILayout.Vector2IntField("", gridSize);
            boxSpacing = EditorGUILayout.FloatField("Box Spacing: ", boxSpacing);

            if (GUILayout.Button("Save Data Grid"))
            {
                string path = EditorUtility.SaveFilePanelInProject("Save grid data", "GridData","asset", "Please enter a file name to save grid data to");

                if(!string.IsNullOrEmpty(path))
                {
                    AssetDatabase.CreateAsset(gridData, path);
                }
            }

            if (currentSelectedGameObject != null)
            {
                DrawObjectDetails(currentSelectedGameObject);
            }

            if (GUILayout.Button("Clear All"))
            {
                currentSelectedGameObject = null;
                assetSearchResults.Clear();
            }
            
            GUILayout.Label("Search Assets:");
            GUILayout.BeginHorizontal(); 
            searchField = EditorGUILayout.TextField(searchField, GUILayout.ExpandWidth(true));
            if (GUILayout.Button("Search", GUILayout.MaxWidth(60)))
            {
                assetSearchResults.Clear();
                string[] guids = AssetDatabase.FindAssets(searchField, new[] { "Assets" });
                foreach (string guid in guids)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                    GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                    if (asset != null)
                    {
                        assetSearchResults.Add(asset);
                    }
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Label("Search Results:");
            scrollViewVector = GUILayout.BeginScrollView(scrollViewVector, GUILayout.MaxHeight(150));

            foreach (GameObject asset in assetSearchResults)
            {
                if (GUILayout.Button(asset.name))
                {
                    EditorGUIUtility.PingObject(asset);
                    Selection.activeObject = asset;
                }
            }

            GUILayout.EndScrollView();

            GUILayout.EndVertical();

            GUILayout.BeginVertical();

            for (int y = 0; y < gridSize.y; y++)
            {
                GUILayout.BeginHorizontal();
                for (int x = 0; x < gridSize.x; x++)
                {
                    string positionStr = $"({x},{y})";
                    GameObject go;
                    gridData.grid.TryGetValue(positionStr, out go);
                    GUILayout.BeginVertical(GUILayout.Width(64 + boxSpacing));

                    GUILayout.Label(go?.name, GUILayout.Width(64));
                    GUIContent content = new GUIContent()
                    {
                        image = go != null ? go.GetComponent<SpriteRenderer>().sprite.texture : null
                    };
                    
                    if (GUILayout.Button(content, GUILayout.Width(64), GUILayout.Height(64)))
                    {
                        Debug.Log("Button at position " + positionStr + " clicked.");
                        
                        var selectedObj = Selection.activeObject as GameObject;
                        if (selectedObj != null)
                        {
                            currentSelectedGameObject = selectedObj;
                            gridData.grid[positionStr] = selectedObj;
                            Debug.Log("Added new object: " + selectedObj.name);
                        }
                    }
                    
                    GUILayout.EndVertical();
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();
        }

        private void DrawObjectDetails(GameObject gameObject)
        {
            Component[] components = gameObject.GetComponents<Component>();
            GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(150)); 

            foreach (Component component in components)
            {
                if (component.GetType() != typeof(Transform))
                {
                    GUIStyle myStyle = new GUIStyle(GUI.skin.label);
                    myStyle.fontStyle = FontStyle.Bold;
                    myStyle.fontSize = 12;

                    Color originalColor = GUI.color;

                    GUI.color = Color.green;  
                    GUILayout.Label(component.GetType().Name, myStyle);
                    GUI.color = originalColor;

                    var fields = component.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

                    foreach(var field in fields)
                    {
                        GUILayout.Label(field.Name + ": " + field.GetValue(component));
                    }

                    GUILayout.Space(10);
                }
            }        

            GUILayout.EndVertical();
        }
    }
}