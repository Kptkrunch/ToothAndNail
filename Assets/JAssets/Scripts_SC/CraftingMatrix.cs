using Fusion;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class CraftingMatrix : MonoBehaviour
	{
		public static CraftingMatrix instance;
		
		[Header("Crafting Matrix")][ShowInInspector]
		public SerializableDictionary <string, MatrixRow> matrix = new();

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
			var craftingDictionary = matrix[row];
			var recipeItem = craftingDictionary.row[item].resultItem;
			return recipeItem;
		}
	}
}
