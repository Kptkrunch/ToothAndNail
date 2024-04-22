using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC.Lists
{
	public class CraftingMatrix : MonoBehaviour
	{
		public static CraftingMatrix instance;
		
		[Header("Crafting Matrix")][ShowInInspector]
		public Dictionary<string, MatrixRow> matrix = new();

		private void Awake()
		{
			instance = this;
		}

		public string GetRecipeFromMatrix(string row, string item)
		{
			var craftingDictionary = matrix[row + "Row"];
			if (craftingDictionary == null) return "";
			var recipeItem = craftingDictionary.row[item].name;
			return recipeItem;
		}
	}
}
