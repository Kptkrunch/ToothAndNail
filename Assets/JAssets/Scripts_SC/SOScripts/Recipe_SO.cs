using UnityEngine;

namespace JAssets.Scripts_SC.SOScripts
{
	[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
	public class Recipe_SO : ScriptableObject
	{
		public Sprite sprite;
		public string resultItem;
	}
}