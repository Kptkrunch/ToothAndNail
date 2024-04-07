using Photon.Pun;
using UnityEngine;

namespace JAssets.Scripts_SC.Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        public static PlayerSpawner instance;

        public GameObject playerPrefab;
        private GameObject player;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            if (PhotonNetwork.IsConnected) SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            var spawnPoint = SpawningController.instance.GetSpawnPoint();
            player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        }
    }
}