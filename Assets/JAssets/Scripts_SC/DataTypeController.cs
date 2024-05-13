using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class DataTypeController : MonoBehaviour
	{
		private static DataTypeController instance;

		private void Awake()
		{
			if(instance != null)
			{
				Destroy(gameObject);
				return;
			}
        
			instance = this;
		}

		public struct PlayerNumAndTagData
		{
			public string PlayerTag;
			public int PlayerNumber;
			public LayerMask PlayerLayer;
		} 
	}
}
