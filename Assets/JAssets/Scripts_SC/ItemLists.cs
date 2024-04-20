using System.Collections.Generic;
using JAssets.Scripts_SC.SOScripts;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC
{
    public class ItemLists : MonoBehaviour
    {
        public static ItemLists instance;

        [Header("All Items List")] [ShowInInspector]
        public List<GameObject> itemList;
        [Header("Particles List")] [ShowInInspector]
        public List<MMSimpleObjectPooler> particleList = new();
        [Header("Pickups List")] [ShowInInspector]
        public List<MMSimpleObjectPooler> pickupList = new();
        [Header("Consumables List")] [ShowInInspector]
        public List<MMSimpleObjectPooler> consumablesList = new();

        public List<LootTable_SO> lootTableList = new();

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

        private void Start()
        {
            PopulateLibrary();
        }

        private void PopulateLibrary()
        {
            foreach (var i in itemList)
            {
                Library.instance.itemDict[i.name] = i;
            }
            
            foreach (var p in pickupList) Library.instance.pickupsDict[p.GameObjectToPool.name] = p;

            foreach (var p in particleList) Library.instance.particleDict[p.GameObjectToPool.name] = p;
            
            foreach (var c in consumablesList) Library.instance.consumableDict[c.GameObjectToPool.name] =  c;

            foreach (var l in lootTableList)
            {
                Library.instance.lootTableDict[l.name] = l;
            }
        }
    }
}