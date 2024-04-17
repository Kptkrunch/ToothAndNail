using System.Collections.Generic;
using JAssets.Scripts_SC.Items;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace JAssets.Scripts_SC
{
    public class EquippedGear : MonoBehaviour
    {
        [Header("Tools")][ShowInInspector]
        public Dictionary<string, Tool> tools;
        [Header("Weapons")] [ShowInInspector] 
        public Dictionary<string, Weapon> weapons;
        [Header("Consumables")] [ShowInInspector]
        public Dictionary<string, Consumable> consumables;

        [Header("Player Tools List")] [SerializeField]
        private List<Tool> toolsList;
        [Header("Player Weapons List")] [SerializeField]
        private List<Weapon> weaponsList;
        [Header("Player Consumables List")] [SerializeField]
        private List<Consumable> consumableList;
        private void Start()
        {
            PopulatePlayerDictionaries();
        }
        private void PopulatePlayerDictionaries()
        {
            tools = new Dictionary<string, Tool>();
            weapons = new Dictionary<string, Weapon>();
            consumables = new Dictionary<string, Consumable>();
            
            foreach (var tool in toolsList)
            {
                tools.Add(tool.name, tool);
            }
            foreach (var weapon in weaponsList)
            {
                weapons.Add(weapon.name, weapon);
            }
            foreach (var consumable in consumableList)
            {
                consumables.Add(consumable.name, consumable);
            }
        }
    }
}