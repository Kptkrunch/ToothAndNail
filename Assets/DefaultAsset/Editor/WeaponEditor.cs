using UnityEditor; 
using UnityEngine; 

[CustomEditor(typeof(Weapon))] 
public class WeaponEditor : Editor 
{ 
    public override void OnInspectorGUI() 
    { 
        Weapon weapon = (Weapon)target; 

        EditorGUILayout.BeginVertical("box"); 

        weapon.weaponName = EditorGUILayout.TextField("Weapon Name", weapon.weaponName); 
        weapon.attackPower = EditorGUILayout.FloatField("Attack Power", weapon.attackPower); 
        weapon.attackSpeed = EditorGUILayout.FloatField("Attack Speed", weapon.attackSpeed); 

        EditorGUILayout.EndVertical(); 

        if(GUI.changed)
        {
            EditorUtility.SetDirty(weapon);
        }
    } 
}