using System.Collections.Generic;
using JAssets.Scripts_SC.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class ConsumableDictionary : MonoBehaviour

	{
	public string consumableName = "item";
	public Consumable consumable;
	[ShowInInspector] public Dictionary<string, Dictionary<string, Consumable>> craftingRow = new();
	
	private void AddToCraftingRow()
		{
			if (!craftingRow.ContainsKey(consumableName))
			{
				craftingRow.Add(consumableName, new Dictionary<string, Consumable>());
			}
			craftingRow[consumableName].Add(consumable.name, consumable);
		}

	private void OnAwake()
	{
		AddToCraftingRow();
	}
	}
}