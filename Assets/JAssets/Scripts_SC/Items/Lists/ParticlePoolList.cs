using System.Collections;
using System.Collections.Generic;
using JAssets.Scripts_SC;
using MoreMountains.Tools;
using UnityEngine;

public class ParticlePoolList : MonoBehaviour
{
	public List<MMSimpleObjectPooler> particleList;
	public MMSimpleObjectPooler Blood1;
	public MMSimpleObjectPooler Blood2;
	public MMSimpleObjectPooler Blood3;
	public MMSimpleObjectPooler Goo1;
	public MMSimpleObjectPooler Goo2;
	public MMSimpleObjectPooler Goo3;
	public MMSimpleObjectPooler Sparks1;
	public MMSimpleObjectPooler Sparks2;
	public MMSimpleObjectPooler Sparks3;
	public MMSimpleObjectPooler WeaponBreak;
	public MMSimpleObjectPooler ItemPickupFlash;
	public MMSimpleObjectPooler PickupSparkle;
	
	private void Start()
	{
		PopulateList();
		PopulateLibrary();
	}
	
	private void PopulateLibrary()
	{
		foreach (var pool in particleList)
		{
			var currentPool = pool.GetPooledGameObject().name;
			if (!Library.instance.particleDict.ContainsKey(currentPool)) ;
			Library.instance.particleDict.Add(currentPool, pool);
		}
	}

	private void PopulateList()
	{
		particleList.Add(Blood1);
		particleList.Add(Blood2);
		particleList.Add(Blood3);
		particleList.Add(Goo1);
		particleList.Add(Goo2);
		particleList.Add(Goo3);
		particleList.Add(Sparks1);
		particleList.Add(Sparks2);
		particleList.Add(Sparks3);
		particleList.Add(WeaponBreak);
		particleList.Add(ItemPickupFlash);
		particleList.Add(PickupSparkle);
	}
}
