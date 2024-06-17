using System.Collections.Generic;
using JAssets.Scripts_SC.Multiplayer;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;


namespace JAssets.Scripts_SC
{
	public class PlayerList : MonoBehaviour
	{
		public static PlayerList Instance;
		public List<Transform> playerSpawnPoints;
		[SerializeField] private GameObject playerObjectToSpawn;
		public PlayerInputManager playerInput;
		private void Awake()
		{
			if (!Instance)
			{
				Instance = this;
			}
			DontDestroyOnLoad(this);
		}

		private void Start()
		{
			SpawnInitialPlayers();
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

		private void SpawnInitialPlayers()
		{
			for (var i = 0; i < PlayerSessionData.TotalPlayers; i++)
			{
				var location = Random.Range(0, playerSpawnPoints.Count);
				// var playerSpawnLocation = playerSpawnPoints[location];
				playerInput.joinAction();
			}

			Debug.Log(PlayerSessionData.TotalPlayers + ": total players to spawn");
		}
	
		public void ClearPlayerList()
		{
			playersList.Clear();
		}
	}
}
