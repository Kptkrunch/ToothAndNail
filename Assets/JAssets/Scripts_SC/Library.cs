using System.Collections.Generic;
using JAssets.Scripts_SC.Items;
using MoreMountains.Tools;
using UnityEngine;

namespace JAssets.Scripts_SC
{
    public class Library : MonoBehaviour
    {
        public static Library instance;
        public Dictionary<string, MMSimpleObjectPooler> particleDict = new();
        public Dictionary<string, MMSimpleObjectPooler> pickupsDict = new();
        public Dictionary<string, MMSimpleObjectPooler> toolEffectsDict = new();
        public Dictionary<string, Tool> toolsDict = new();

        public Dictionary<string, Weapon> weaponsDict = new();

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

        public void AddWeapon(string name, Weapon weapon)
        {
            weaponsDict.Add(name, weapon);
        }

        public void AddTool(string name, Tool tool)
        {
            toolsDict.Add(name, tool);
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
    }
}