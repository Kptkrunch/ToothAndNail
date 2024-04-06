using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScaneHierarchy : Editor 
{
    static List<GameObject> missingObjects = new List<GameObject>();

    [MenuItem("Tools/Find and Fix Missing References")]
    public static void FindAndFixMissingReferencesInScene()
    {
        missingObjects.Clear();

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(); 

        foreach (var gameObject in allObjects)
        {
            Component[] components = gameObject.GetComponents<Component>();

            foreach (var component in components)
            {
                if (!component)
                {
                    Debug.LogError("Missing Component in gameObject: " + gameObject.name, gameObject);
                    continue;
                }

                SerializedObject so = new SerializedObject(component);
                var sp = so.GetIterator();

                while (sp.NextVisible(true))
                {
                    if (sp.propertyType == SerializedPropertyType.ObjectReference && sp.objectReferenceValue == null && sp.objectReferenceInstanceIDValue != 0)
                    {
                        GameObject foundObject = SearchForMatchingObject(gameObject, sp);

                        if (foundObject != null)
                        {
                            sp.objectReferenceValue = foundObject;
                            Debug.Log("Fixed missing reference in: " + gameObject.name + ", Component: " + component.GetType().Name + ", Property : " + sp.name + ", Assigned Object: " + foundObject.name);
                        }
                        else
                        {
                            missingObjects.Add(gameObject);
                            ShowError(component, gameObject, sp);
                        }
                        so.ApplyModifiedProperties();
                    }
                }
            }
        }
    }
    
    private static void ShowError(Component component, GameObject gameObject, SerializedProperty prop)
    {
        Debug.LogError("Missing reference found in: " + gameObject.name + ", Component: " + component.GetType().Name + ", Property : " + prop.name, gameObject);
    }

    private static GameObject SearchForMatchingObject(GameObject gameObject, SerializedProperty sp)
    {
        GameObject foundObject = null;
        
        // Search children
        foundObject = gameObject.transform.Find(sp.name)?.gameObject;
           
        // If not found, search parent
        if (foundObject == null && gameObject.transform.parent != null)
        {
            if (gameObject.transform.parent.name == sp.name)
            {
                foundObject = gameObject.transform.parent.gameObject;
            }
        }

        return foundObject;
    }

    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void RenderCustomGizmo(GameObject gameObject, GizmoType gizmoType)
    {
        if (missingObjects.Contains(gameObject))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(gameObject.transform.position, gameObject.transform.localScale);
        }
    }
}