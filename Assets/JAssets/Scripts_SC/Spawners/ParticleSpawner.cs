using UnityEngine;

namespace JAssets.Scripts_SC.Spawners
{
    public class ParticleSpawner : MonoBehaviour
    {
        public static ParticleSpawner instance;

        private void Awake()
        {
            instance = this;
        }
    }
}