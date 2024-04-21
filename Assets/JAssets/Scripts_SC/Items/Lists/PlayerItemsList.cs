using System.Collections.Generic;
using UnityEngine;

namespace JAssets.Scripts_SC.Items.Lists
{
	public class PlayerItemsList : MonoBehaviour
	{

		public List<GameObject> itemsList = new();

		public GameObject BattleAx;
		public GameObject Spear;
		public GameObject Sword;
		public GameObject Bow;
		public GameObject SmokeBomb;
		public GameObject Stick;
		public GameObject Stone;
		public GameObject Vine;
	
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
				if (!Library.instance.itemDict.ContainsKey(currentItem)) ;
				Library.instance.itemDict.Add(currentItem, item);
			}
		}

		private void PopulateList()
		{
			itemsList.Add(BattleAx);
			itemsList.Add(Spear);
			itemsList.Add(Sword);
			itemsList.Add(Bow);
			itemsList.Add(SmokeBomb);
			itemsList.Add(Stick);
			itemsList.Add(Stone);
			itemsList.Add(Vine);
		}
	}
}
