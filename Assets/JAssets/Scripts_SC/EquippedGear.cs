using System.Collections.Generic;
using JAssets.Scripts_SC.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC
{
    public class EquippedGear : MonoBehaviour
    {
        [Header("Tools")][ShowInInspector]
        public Dictionary<string, Tool> tools;
        [Header("Weapons")][ShowInInspector]
        public Dictionary<string, Weapon> weapons;

        [Header("Player Tools List")] [SerializeField]
        private List<Tool> toolsList;
        [Header("Player Weapons List")] [SerializeField]
        private List<Weapon> weaponsList;

        private void Start()
        {
            PopulatePlayerDictionaries();
        }
        private void PopulatePlayerDictionaries()
        {
            tools = new Dictionary<string, Tool>();
            weapons = new Dictionary<string, Weapon>();
            foreach (var tool in toolsList)
            {
                tools.Add(tool.name, tool);
            }
            foreach (var weapon in weaponsList)
            {
                weapons.Add(weapon.name, weapon);
            }
        }
    }
}