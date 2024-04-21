using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace JAssets.Scripts_SC.Items.Lists
{
	public class ConsumablePoolList : MonoBehaviour
	{
		public List<MMSimpleObjectPooler> consumableList = new();
		public MMSimpleObjectPooler CBolos;
		public MMSimpleObjectPooler CBone;
		public MMSimpleObjectPooler CCaltrops;
		public MMSimpleObjectPooler CCross;
		public MMSimpleObjectPooler CNet;
		public MMSimpleObjectPooler CScrap;
		public MMSimpleObjectPooler CSmokeBomb;
		public MMSimpleObjectPooler CSnareTrap;
		public MMSimpleObjectPooler CSpikeTrap;
		public MMSimpleObjectPooler CStick;
		public MMSimpleObjectPooler CStone;
		public MMSimpleObjectPooler CVine;
	
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
				if (!Library.instance.consumableDict.ContainsKey(currentPool)) ;
				Library.instance.consumableDict.Add(currentPool, pool);
			}
		}

		private void PopulateList()
		{

		}
	}
}
