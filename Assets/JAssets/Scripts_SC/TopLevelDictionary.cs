using System.Collections.Generic;
using JAssets.Scripts_SC.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class TopLevelDictionary : MonoBehaviour
	{
		public string dictionaryName;
		public Consumable consumable;
		public SpriteRenderer spriteRenderer;
		[Header("Dictionary Row")][ShowInInspector]
		public TopLevelDictionary dictionary ;
	}
}