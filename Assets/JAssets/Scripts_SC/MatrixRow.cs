using JAssets.Scripts_SC.SOScripts;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

namespace JAssets.Scripts_SC
{
	public class MatrixRow : MonoBehaviour
	{
		[Header("Crafting Matrix")][ShowInInspector]
		public SerializedDictionary<string, Recipe_SO> row = new();
	}
}