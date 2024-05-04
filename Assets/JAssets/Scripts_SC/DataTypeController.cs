using System;
using System.Collections;
using System.Collections.Generic;
using AssetInventory;
using UnityEngine;

public class DataTypeController : MonoBehaviour
{
	public static DataTypeController instance;

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
