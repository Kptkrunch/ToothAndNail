using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC.SOScripts
{
	[Serializable]
	[CreateAssetMenu(fileName = "New GridData", menuName = "GridData")]
	public class GridData : SerializedScriptableObject
	{
		//test
		[SerializeField] public int gridWidth = 3;

		[SerializeField] public int gridHeight = 3;

		[ShowInInspector] public Dictionary<string, GameObject> grid = new();
	}
}