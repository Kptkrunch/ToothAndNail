using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAssets.Scripts_SC
{
	public class PlayerList : MonoBehaviour
	{
		public static PlayerList Instance;
		[SerializeField] private GameObject playerObjectToSpawn;

		private void Awake()
		{
			if (!Instance)
			{
				Instance = this;
			}
			DontDestroyOnLoad(Instance);
		}
		
		public List<GameObject> playersList = new();

		public DataTypeController.PlayerNumAndTagData AddPlayerToList(GameObject playerPrefab)
		{
			DataTypeController.PlayerNumAndTagData playerData;
			playersList.Add(playerPrefab);
			var playerLayer = LayerMask.NameToLayer("Player" + playersList.Count);
			var playerTag = "Player" + playersList.Count;
			playerPrefab.tag = playerTag;
			playerPrefab.gameObject.layer = playerLayer;
			playerData.PlayerNumber = playersList.Count;
			playerData.PlayerTag = playerTag;
			playerData.PlayerLayer = playerLayer;
			return playerData;
		}
		
		public void ClearPlayerList()
		{
			playersList.Clear();
		}

		public void AddPlayerToJoinScreen()
		{
			var newPlayer = Instantiate(playerObjectToSpawn);
			newPlayer.SetActive(false);
			AddPlayerToList(newPlayer);
		}
		public void RemovePlayerFromList(InputAction.CallbackContext context, GameObject playerPrefab)
		{
			playersList.Remove(playerPrefab);
		}
	}
}
