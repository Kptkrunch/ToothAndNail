using System.Collections.Generic;
using JAssets.Scripts_SC.Items;
using MoreMountains.Tools;
using UnityEngine;

namespace JAssets.Scripts_SC.Lists
{
	public class ConsumablePoolList : MonoBehaviour
	{
		public List<MMSimpleObjectPooler> consumableList = new();
		
		public MMSimpleObjectPooler cBolos;
		public MMSimpleObjectPooler cBone;
		public MMSimpleObjectPooler cCaltrops;
		public MMSimpleObjectPooler cCross;
		public MMSimpleObjectPooler cNet;
		public MMSimpleObjectPooler cScrap;
		public MMSimpleObjectPooler cSmokeBomb;
		public MMSimpleObjectPooler cSnareTrap;
		public MMSimpleObjectPooler cSpikeTrap;
		public MMSimpleObjectPooler cStick;
		public MMSimpleObjectPooler cStone;
		public MMSimpleObjectPooler cVine;
		public MMSimpleObjectPooler cArrow;
	
		
		private void Start()
		{
			PopulateList();
			PopulateLibrary();
		}
		
		private void PopulateLibrary()
		{
			foreach (var pool in consumableList)
			{
				var currentPool = pool.GetPooledGameObject().name;
				if (Library.instance.consumableDict != null &&
					!Library.instance.consumableDict.ContainsKey(currentPool))
				{
					Library.instance.consumableDict.TryAdd(currentPool, pool);
				}
			}
		}

		private void PopulateList()
		{
			consumableList.Add(cArrow);
			consumableList.Add(cBolos);
			consumableList.Add(cBone);
			consumableList.Add(cCaltrops);
			consumableList.Add(cCross);
			consumableList.Add(cNet);
			consumableList.Add(cScrap);
			consumableList.Add(cSmokeBomb);
			consumableList.Add(cSnareTrap);
			consumableList.Add(cSpikeTrap);
			consumableList.Add(cStick);
			consumableList.Add(cStone);
			consumableList.Add(cVine);
		}
	}
}
