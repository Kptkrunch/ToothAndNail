using System;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class SpawnLocation : MonoBehaviour, ISpawnLocationItems
	{
		public event Action<ISpawnLocationItems> SpawnRequested = delegate { };
		public string locationType;
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

		public event Action<ISpawnLocationItems> requestItemSpawn;
		public bool IsActive { get; set; }
	}
}