using Fusion;
using JAssets.Scripts_SC.SOScripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class CraftingMatrix : MonoBehaviour
	{
		public static CraftingMatrix instance;
		
		[ShowInInspector] [SerializeField] private Recipe_SO defaultRecipe;
		
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
			if (!matrix[row].row.TryGetValue(item, out var fromMatrix)) return "";
			Debug.Log(row);
			Debug.Log(item);
			Debug.Log(fromMatrix);
			Debug.Log(4);

			return fromMatrix.name;

		}
	}
}
