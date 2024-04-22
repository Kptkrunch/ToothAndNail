using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC
{
    public class EquippedGear : MonoBehaviour
    {
        [Header("Items")] [ShowInInspector] 
        public Dictionary<string, GameObject> items = new();
        
        [Header("Player Items List")] [SerializeField]
        private List<GameObject> itemList;

        private void Start()
        {
            PopulatePlayerDictionaries();
        }
        private void PopulatePlayerDictionaries()
        {
            items = new Dictionary<string, GameObject>();

            foreach (var item in itemList)
            {
                items.TryAdd(item.name, item);
            }
        }
    }
}