using System.Collections.Generic;
using JAssets.Scripts_SC.SOScripts;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

namespace JAssets.Scripts_SC.Lists
{
	public class MatrixRow : MonoBehaviour
	{
		[Header("Crafting Matrix")][ShowInInspector]
		public Dictionary<string, Recipe_SO> row = new();
	}
}