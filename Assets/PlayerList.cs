using System;
using System.Collections;
using System.Collections.Generic;
using JAssets.Scripts_SC;
using UnityEngine;

public class PlayerList : MonoBehaviour
{
	public static PlayerList instance;

	private void Awake()
	{
		instance = this;
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
	
	public void RemovePlayerFromList(GameObject playerPrefab)
	{
		playersList.Remove(playerPrefab);
	}
}
