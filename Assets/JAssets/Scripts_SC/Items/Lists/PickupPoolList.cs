using System;
using System.Collections;
using System.Collections.Generic;
using JAssets.Scripts_SC;
using MoreMountains.Tools;
using UnityEngine;

public class PickupPoolList : MonoBehaviour
{
	public List<MMSimpleObjectPooler> pickupList;
	public MMSimpleObjectPooler PVine;
	public MMSimpleObjectPooler PStone;
	public MMSimpleObjectPooler PBone;
	public MMSimpleObjectPooler PStick;
	public MMSimpleObjectPooler PScrap;
	public MMSimpleObjectPooler PBattleAx;
	public MMSimpleObjectPooler PSword;
	public MMSimpleObjectPooler PSpear;
	public MMSimpleObjectPooler PBow;

	private void Start()
	{
		PopulateList();
		PopulateLibrary();
	}

	private void PopulateLibrary()
	{
		foreach (var pool in pickupList)
		{
			var currentPool = pool.GetPooledGameObject().name;
			if (!Library.instance.pickupsDict.ContainsKey(currentPool)) ;
			Library.instance.pickupsDict.Add(currentPool, pool);
		}
	}

	private void PopulateList()
	{
		pickupList.Add(PVine);
		pickupList.Add(PStone);
		pickupList.Add(PBone);
		pickupList.Add(PStick);
		pickupList.Add(PScrap);
		pickupList.Add(PBattleAx);
		pickupList.Add(PSword);
		pickupList.Add(PSpear);
		pickupList.Add(PBow);
	}
}
