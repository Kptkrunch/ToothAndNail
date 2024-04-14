using UnityEngine;
using System.Collections.Generic;
using JAssets.Scripts_SC;
using JAssets.Scripts_SC.Items;
using Sirenix.OdinInspector;

public class ConsumableMatrix : MonoBehaviour
{
	[Header("Recipe Matrix")] [ShowInInspector]
	public Dictionary<string, ConsumableDictionary> dict;
}

