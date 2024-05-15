using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class DataTypeController : MonoBehaviour
	{
		private static DataTypeController _instance;

		private void Awake()
		{
			if(_instance != null)
			{
				Destroy(gameObject);
				return;
			}
        
			_instance = this;
		}

		public struct PlayerNumAndTagData
		{
			public string PlayerTag;
			public int PlayerNumber;
			public LayerMask PlayerLayer;
		} 
	}
}
