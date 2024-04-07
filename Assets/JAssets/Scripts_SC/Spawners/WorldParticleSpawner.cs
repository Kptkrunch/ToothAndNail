using MoreMountains.Tools;
using UnityEngine;

namespace JAssets.Scripts_SC.Spawners
{
    public class WorldParticleSpawner : MonoBehaviour
    {
        public static WorldParticleSpawner instance;
        public MMSimpleObjectPooler toolBreakParticle;

        public MMSimpleObjectPooler weaponPickupParticle;
        public MMSimpleObjectPooler weaponsBreakParticle;

        private void Awake()
        {
            instance = this;
        }
    }
}