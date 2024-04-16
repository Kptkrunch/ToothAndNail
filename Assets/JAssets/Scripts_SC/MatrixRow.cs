using Fusion;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class MatrixRow : MonoBehaviour
	{
		[Header("Crafting Matrix")][ShowInInspector]
		public SerializableDictionary <string, Recipe_SO> row = new();
	}
}
