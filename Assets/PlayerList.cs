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

	public int AddPlayerToList(GameObject playerPrefab)
	{
		playersList.Add(playerPrefab);
		playerPrefab.layer = LayerMask.NameToLayer("Player" + playersList.Count);
		playerPrefab.gameObject.tag = "Player" + playersList.Count;
		return playersList.Count;
	}
	
	public void RemovePlayerFromList(GameObject playerPrefab)
	{
		playersList.Remove(playerPrefab);
	}
}
