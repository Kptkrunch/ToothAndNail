using System.Collections.Generic;
using JAssets.Scripts_SC.Items;
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
        [Header("Tool Effects")][ShowInInspector]
        public Dictionary<string, MMSimpleObjectPooler> toolEffectsDict = new();
        [Header("Consumables")] [ShowInInspector]
        public Dictionary<string, MMSimpleObjectPooler> consumableDict = new();

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

        public void AddPickup(string name, MMSimpleObjectPooler pickupPool)
        {
            pickupsDict.Add(name, pickupPool);
        }

        public void AddToolEffect(string name, MMSimpleObjectPooler toolEffectPool)
        {
            toolEffectsDict.Add(name, toolEffectPool);
        }

        public void AddParticle(string name, MMSimpleObjectPooler particlePool)
        {
            particleDict.Add(name, particlePool);
        }
        public void AddConsumable(string name, MMSimpleObjectPooler consumablesPool)
        {
            particleDict.Add(name, consumablesPool);
        }
    }
}