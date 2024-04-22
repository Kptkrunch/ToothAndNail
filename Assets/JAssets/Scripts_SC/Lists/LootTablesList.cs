using System.Collections.Generic;
using JAssets.Scripts_SC.SOScripts;
using UnityEngine;

namespace JAssets.Scripts_SC.Lists
{
	public class LootTablesList : MonoBehaviour
	{
		public List<LootTable_SO> lootTableList;

		public LootTable_SO boneNode;
		public LootTable_SO grassNode;
		public LootTable_SO rootNode;
		public LootTable_SO weaponNode;
		public LootTable_SO gearNode;
		public LootTable_SO rubbleNode;

		private void Start()
		{
			PopulateList();
			PopulateLibrary();
		}
		private void PopulateLibrary()
		{
			foreach (var pool in lootTableList)
			{
				var currentPool = pool.name;
				Library.instance.lootTableDict.Add(pool.name, pool);
			}
		}
		private void PopulateList()
		{
			lootTableList.Add(boneNode);
			lootTableList.Add(grassNode);
			lootTableList.Add(rootNode);
			lootTableList.Add(weaponNode);
			lootTableList.Add(gearNode);
			lootTableList.Add(rubbleNode);
		}
	}
}
