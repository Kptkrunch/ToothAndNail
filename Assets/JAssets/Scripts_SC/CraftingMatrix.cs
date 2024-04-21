using System.Collections.Generic;
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
			var fromMatrix = defaultRecipe;
			if (matrix.ContainsKey(row))
			{
				if (matrix[row].row.ContainsKey(item))
				{
					fromMatrix = matrix[row].row[item];
					return fromMatrix.name;
				}
			}

			return fromMatrix.name;

		}
	}
}
