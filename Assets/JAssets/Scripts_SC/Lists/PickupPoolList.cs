using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace JAssets.Scripts_SC.Lists
{
	public class PickupPoolList : MonoBehaviour
	{
		public List<MMSimpleObjectPooler> pickupList;
		public MMSimpleObjectPooler pVine;
		public MMSimpleObjectPooler pStone;
		public MMSimpleObjectPooler pBone;
		public MMSimpleObjectPooler pStick;
		public MMSimpleObjectPooler pScrap;
		public MMSimpleObjectPooler pBattleAx;
		public MMSimpleObjectPooler pSword;
		public MMSimpleObjectPooler pSpear;
		public MMSimpleObjectPooler pBow;

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
				if (Library.instance.pickupsDict != null && !Library.instance.pickupsDict.ContainsKey(currentPool)) ;
				if (Library.instance.pickupsDict != null) Library.instance.pickupsDict.Add(currentPool, pool);
			}
		}

		private void PopulateList()
		{
			pickupList.Add(pVine);
			pickupList.Add(pStone);
			pickupList.Add(pBone);
			pickupList.Add(pStick);
			pickupList.Add(pScrap);
			pickupList.Add(pBattleAx);
			pickupList.Add(pSword);
			pickupList.Add(pSpear);
			pickupList.Add(pBow);
		}
	}
}
