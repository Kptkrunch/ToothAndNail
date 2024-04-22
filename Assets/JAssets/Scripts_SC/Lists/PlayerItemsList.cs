using System.Collections.Generic;
using UnityEngine;

namespace JAssets.Scripts_SC.Lists
{
	public class PlayerItemsList : MonoBehaviour
	{

		public List<GameObject> itemsList = new();

		public GameObject battleAx;
		public GameObject spear;
		public GameObject sword;
		public GameObject bow;
		public GameObject smokeBomb;
		public GameObject stick;
		public GameObject stone;
		public GameObject vine;
	
		private void Start()
		{
			PopulateList();
			PopulateLibrary();
		}
		
		private void PopulateLibrary()
		{
			foreach (var item in itemsList)
			{
				var currentItem = item.name;
				if (Library.instance.itemDict != null && !Library.instance.itemDict.ContainsKey(currentItem)) ;
				if (Library.instance.itemDict != null) Library.instance.itemDict.Add(currentItem, item);
			}
		}

		private void PopulateList()
		{
			itemsList.Add(battleAx);
			itemsList.Add(spear);
			itemsList.Add(sword);
			itemsList.Add(bow);
			itemsList.Add(smokeBomb);
			itemsList.Add(stick);
			itemsList.Add(stone);
			itemsList.Add(vine);
		}
	}
}
