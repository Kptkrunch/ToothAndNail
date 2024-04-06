using UnityEditor;
using System.IO;
using UnityEngine;

public class OrganizeAssetsFolder : EditorWindow
{
    string assetPath = "Assets";

    [MenuItem("Tools/Organize Assets by Type")]
    public static void ShowWindow()
    {
        GetWindow<OrganizeAssetsFolder>();
    }

    void OnGUI()
    {
        GUILayout.Label("Enter the path to organize assets, and then press the button");
        assetPath = EditorGUILayout.TextField("Path to assets", assetPath);

        if (GUILayout.Button("Organize All Assets"))
        {
            assetPath = "Assets";
            OrganizeAssets();
        }

        if (GUILayout.Button("Organize Assets"))
        {
            OrganizeAssets();
        }
    }

    void OrganizeAssets()
    {
        var assetGuids = AssetDatabase.FindAssets("", new[] { assetPath });

        foreach (var assetGuid in assetGuids)
        {
            var path = AssetDatabase.GUIDToAssetPath(assetGuid);
            var asset = AssetDatabase.LoadMainAssetAtPath(path);

            if (path.Contains("/Plugins/"))
                continue;

            string folderName;

            if (asset.GetType().ToString().Contains("."))
                folderName = asset.GetType().ToString().Substring(asset.GetType().ToString().LastIndexOf('.') + 1);
            else
                folderName = asset.GetType().ToString();

            folderName = char.ToUpper(folderName[0]) + folderName.Substring(1);

            var folderPath = assetPath + "/" + folderName;
            var newPath = folderPath + "/" + Path.GetFileName(path);

            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                AssetDatabase.CreateFolder(assetPath, folderName);
            }

            if(newPath != path)
            {
                AssetDatabase.MoveAsset(path, newPath);
            }
        }
    }
}