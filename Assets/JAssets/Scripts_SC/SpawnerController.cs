using System.Collections.Generic;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class SpawnerController : MonoBehaviour
	{
		public static SpawnerController instance;
		
		[ShowInInspector] [Header("Spawn Location List")] private List<SpawnLocation> spawnLocationList = new();
		[ShowInInspector] [Header("Node List")] public List<MMSimpleObjectPooler> nodeList = new();

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

		private void Start()
		{
			RandomizeActiveSpawnLocations(spawnLocationList.Count);
		}
		
		private void RandomizeActiveSpawnLocations(int activeSpawnLocationCount)
		{
			foreach (var spawnLocation in spawnLocationList)
			{
				Debug.Log(spawnLocation.name);
				spawnLocation.gameObject.SetActive(false);
			}

			for (int i = 0; i < activeSpawnLocationCount; i++)
			{
				Debug.Log(spawnLocationList[i]);
				spawnLocationList[activeSpawnLocationCount].gameObject.SetActive(true);
				
			}
		}
	}
}
