using System.Collections.Generic;
using JAssets.Scripts_SC.SOScripts;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class SpawnerController : MonoBehaviour
	{
		private static SpawnerController instance { get; set; }
		[SerializeField] private LootTable_SO lootTableSo;

		[SerializeField] private List<ISpawnLocationSpawnLoot> spawnLocationList = new();

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}

		public static void RegisterSpawnPoint(ISpawnLocationSpawnLoot spawnLocationSpawnPoint)
		{
			spawnLocationSpawnPoint.requestItemSpawn += HandleSpawnRequested;
		}

		public static void UnregisterSpawnPoint(ISpawnLocationSpawnLoot spawnLocationSpawnPoint)
		{
			spawnLocationSpawnPoint.requestItemSpawn -= HandleSpawnRequested;
		}

		private static void HandleSpawnRequested(ISpawnLocationSpawnLoot spawnLocationSpawnPoint)
		{
			// Spawn object at spawnLocationSpawnPoint here
		}


		public void RandomizeActiveSpawnPoints(int activeSpawnPointCount)
		{
			// Initially deactivate all spawn points
			foreach (var spawnPoint in spawnLocationList)
			{
				spawnPoint.IsActive = false;
			}

			// Randomly decide which spawn points are active
			for (int i = 0; i < activeSpawnPointCount; i++)
			{
				int index;
				do
				{
					index = Random.Range(0, spawnLocationList.Count);
				} while (spawnLocationList[index].IsActive);

				spawnLocationList[index].IsActive = true;
			}
		}
	}
}
