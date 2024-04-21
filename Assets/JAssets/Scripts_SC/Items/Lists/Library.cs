using System.Collections.Generic;
using JAssets.Scripts_SC.Items;
using JAssets.Scripts_SC.SOScripts;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC
{
    public class Library : MonoBehaviour
    {
        public static Library instance;

        [Header("All Items List")] [ShowInInspector]
        public Dictionary<string, GameObject> itemDict = new();
        [Header("Particles")][ShowInInspector]
        public Dictionary<string, MMSimpleObjectPooler> particleDict = new();
        [Header("Pickups")][ShowInInspector]
        public Dictionary<string, MMSimpleObjectPooler> pickupsDict = new();
        [Header("Consumables")] [ShowInInspector] 
        public Dictionary<string, MMSimpleObjectPooler> consumableDict = new();        
        [Header("Nodes")] [ShowInInspector] 
        public Dictionary<string, MMSimpleObjectPooler> nodesDict = new();
        [Header("Loot Tables")] [ShowInInspector]
        public Dictionary<string, LootTable_SO> lootTableDict = new();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}