using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC
{
    public class EquippedGear : MonoBehaviour
    {
        [Header("Items")] [ShowInInspector] 
        public Dictionary<string, GameObject> Items = new();
        
        [Header("Player Items List")] [SerializeField]
        private List<GameObject> itemList;

        private void Start()
        {
            PopulatePlayerDictionaries();
        }
        private void PopulatePlayerDictionaries()
        {
            Items = new Dictionary<string, GameObject>();

            foreach (var item in itemList)
            {
                Items.TryAdd(item.name, item);
            }
        }
    }
}