using System.Collections;
using System.Collections.Generic;
using JAssets.Scripts_SC;
using JAssets.Scripts_SC.SOScripts;
using MoreMountains.Tools;
using UnityEngine;

public class LootTablesList : MonoBehaviour
{
	public List<LootTable_SO> lootTableList;

	public LootTable_SO BoneNode;
	public LootTable_SO GrassNode;
	public LootTable_SO RootNode;
	public LootTable_SO WeaponNode;
	public LootTable_SO GearNode;
	public LootTable_SO RubbleNode;

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
		lootTableList.Add(BoneNode);
		lootTableList.Add(GrassNode);
		lootTableList.Add(RootNode);
		lootTableList.Add(WeaponNode);
		lootTableList.Add(GearNode);
		lootTableList.Add(RubbleNode);
	}
}
