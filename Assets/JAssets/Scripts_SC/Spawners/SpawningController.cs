using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JAssets.Scripts_SC.Spawners
{
    public class SpawningController : MonoBehaviour
    {
        public static SpawningController instance;

        public GameObject[] locationArray;

        [ShowInInspector] [Header("Spawn Locations")]
        public Dictionary<string, GameObject> locationDictionary = new();

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            FillDictionaries();
        }

        private void FillDictionaries()
        {
            foreach (var l in locationArray)
            {
                locationDictionary[l.name] = l;
                l.SetActive(false);
            }
        }

        public Transform GetSpawnPoint()
        {
            return locationArray[Random.Range(0, locationArray.Length)].transform;
        }
    }
}