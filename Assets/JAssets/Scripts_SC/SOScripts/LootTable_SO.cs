using System.Collections.Generic;
using UnityEngine;

namespace JAssets.Scripts_SC.SOScripts
{
	[CreateAssetMenu(fileName = "New LootTable", menuName = "LootTable")]
	public class LootTable_SO : ScriptableObject
	{
		[System.Serializable]
		public struct LootItem
		{
			public string itemName;
			public int itemWeight;
		}

		public List<LootItem> lootItems;

		public string GetRandomLoot()
		{
			var totalWeight = 0;
			foreach (var item in lootItems)
			{
				totalWeight += item.itemWeight;
			}

			var randomNumber = Random.Range(0, totalWeight);
			foreach (var item in lootItems) 
			{
				if(randomNumber < item.itemWeight) return item.itemName;
				randomNumber -= item.itemWeight;
			}
			return null;
		}
	}
}