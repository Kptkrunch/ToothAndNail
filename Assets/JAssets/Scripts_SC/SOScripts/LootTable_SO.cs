using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New LootTable", menuName = "LootTable")]
public class LootTable_SO : ScriptableObject
{
	[System.Serializable]
	public struct LootItem
	{
		public string Name;
		public int Weight;
	}

	public List<LootItem> LootItems;

	public string GetRandomLoot()
	{
		int totalWeight = 0;
		foreach (LootItem item in LootItems)
		{
			totalWeight += item.Weight;
		}

		int randomNumber = Random.Range(0, totalWeight);
		foreach (LootItem item in LootItems) 
		{
			if(randomNumber < item.Weight) return item.Name;
			randomNumber -= item.Weight;
		}
		return null;
	}
}