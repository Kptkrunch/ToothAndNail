using System.Collections.Generic;
using JAssets.Scripts_SC.Lists;
using Sirenix.OdinInspector;
using UnityEngine;

namespace JAssets.Scripts_SC.Spawners
{
	public class CraftingMaterialNodeSpawnerController : MonoBehaviour
	{
		private static CraftingMaterialNodeSpawnerController instance;
		
		[ShowInInspector] [Header("Spawn Location List")] public List<GameObject> spawnLocationList = new();
		public  List<ItemNode> nodesList;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			RandomizeActiveSpawnLocations(spawnLocationList.Count);
		}

		private void RandomizeNodeType(GameObject spawnLocation)
		{
			int randomIndex = Random.Range(0, nodesList.Count);
			var theNode = Library.instance.nodesDict[nodesList[randomIndex].name + "-0"].GetPooledGameObject();
			if (theNode)
			{
				theNode.transform.position = spawnLocation.transform.position;
				theNode.SetActive(true);
			}
		}
		
		private void RandomizeActiveSpawnLocations(int activeSpawnLocationCount)
		{
			foreach (var spawnLocation in spawnLocationList)
			{
				Debug.Log(spawnLocation.name);
				spawnLocation.gameObject.SetActive(false);
			}

			foreach (var spawnLocation in spawnLocationList)
			{
				Debug.Log(spawnLocation.name);
				var activationChance = Random.Range(0, 10);
				if (activationChance >= 2)
				{
					Debug.Log(spawnLocation.activeInHierarchy);
					Debug.Log(activationChance);

					RandomizeNodeType(spawnLocation.gameObject);
				}
			}
		}
	}
}
