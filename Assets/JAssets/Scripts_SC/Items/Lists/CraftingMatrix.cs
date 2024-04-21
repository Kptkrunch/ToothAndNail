using System.Collections.Generic;
using JAssets.Scripts_SC.SOScripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class CraftingMatrix : MonoBehaviour
	{
		public static CraftingMatrix instance;
		
		[Header("Crafting Matrix")][ShowInInspector]
		public Dictionary<string, MatrixRow> matrix = new();

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}

		public string GetRecipeFromMatrix(string row, string item)
		{
			var craftingDictionary = matrix[row + "Row"];
			var recipeItem = craftingDictionary.row[item].name;
			return recipeItem;
		}
	}
}
