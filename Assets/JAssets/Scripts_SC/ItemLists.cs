using System.Collections.Generic;
using JAssets.Scripts_SC.Items;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC
{
    public class ItemLists : MonoBehaviour
    {
        public static ItemLists instance;

        [Header("Weapons List")] [ShowInInspector]
        public List<Weapon> weaponsList = new();

        [Header("Tools List")] [ShowInInspector]
        public List<Tool> toolList = new();

        [Header("Particles List")] [ShowInInspector]
        public List<MMSimpleObjectPooler> particleList = new();

        [Header("Pickups List")] [ShowInInspector]
        public List<MMSimpleObjectPooler> pickupList = new();

        [Header("Tool Effects List")] [ShowInInspector]
        public List<MMSimpleObjectPooler> toolEffectsList = new();        
        
        [Header("Consumables List")] [ShowInInspector]
        public List<MMSimpleObjectPooler> consumablesList = new();

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
            foreach (var w in weaponsList) Library.instance.weaponsDict[w.name] = w;

            foreach (var t in toolList) Library.instance.toolsDict[t.name] = t;

            foreach (var p in pickupList) Library.instance.pickupsDict[p.GameObjectToPool.name] = p;

            foreach (var p in particleList) Library.instance.particleDict[p.GameObjectToPool.name] = p;

            foreach (var t in toolEffectsList) Library.instance.toolEffectsDict[t.GameObjectToPool.name] = t;
            
            foreach (var c in consumablesList) Library.instance.consumableDict[c.GameObjectToPool.name] =  c;
        }
    }
}