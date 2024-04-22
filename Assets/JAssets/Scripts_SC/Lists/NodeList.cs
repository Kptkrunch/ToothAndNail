using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace JAssets.Scripts_SC.Lists
{
	public class NodeList : MonoBehaviour
	{
		public List<MMSimpleObjectPooler> nodesList;

		public MMSimpleObjectPooler nGrassNode;
		public MMSimpleObjectPooler nGearNode;
		public MMSimpleObjectPooler nBoneNode;
		public MMSimpleObjectPooler nRootNode;
		public MMSimpleObjectPooler nRubbleNode;
		public MMSimpleObjectPooler nDebrisNode;

		private void Start()
		{
			PopulateList();
			PopulateLibrary();
		}

		private void PopulateLibrary()
		{
			foreach (var pool in nodesList)
			{
				Library.instance.nodesDict[pool.GetPooledGameObject().name] = pool;
			}
		}

		private void PopulateList()
		{
			nodesList.Add(nGrassNode);
			nodesList.Add(nGearNode);
			nodesList.Add(nBoneNode);
			nodesList.Add(nRootNode);
			nodesList.Add(nRubbleNode);
			nodesList.Add(nDebrisNode);
		}
	}
}
