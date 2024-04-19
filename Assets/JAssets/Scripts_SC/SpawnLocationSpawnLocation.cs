using System;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class SpawnLocationSpawnLocation : MonoBehaviour, ISpawnLocationSpawnLoot
	{
		public event Action<ISpawnLocationSpawnLoot> SpawnRequested = delegate { };
		[SerializeField] private SpriteRenderer sprite;
		private void Start()
		{
			SpawnerController.RegisterSpawnPoint(this);
		}

		private void OnDestroy()
		{
			SpawnerController.UnregisterSpawnPoint(this);
		}

		public void RequestSpawn()
		{
			SpawnRequested(this);
		}

		public event Action<ISpawnLocationSpawnLoot> requestItemSpawn;
		public bool IsActive { get; set; }
	}
}