using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JAssets.Scripts_SC.Spawners
{
    public class HitParticleSpawner : MonoBehaviour
    {
        public static HitParticleSpawner instance;

        [Header("Blood Splatters")] public MMSimpleObjectPooler bloodSplatter01;

        public MMSimpleObjectPooler bloodSplatter02;
        public MMSimpleObjectPooler bloodSplatter03;
        public List<MMSimpleObjectPooler> bloodSplatterList;

        [Header("Goo Splatters")] public MMSimpleObjectPooler gooSplatter01;

        public MMSimpleObjectPooler gooSplatter02;
        public MMSimpleObjectPooler gooSplatter03;
        public List<MMSimpleObjectPooler> gooSplatterList;

        [Header("Clashes Metal")] public MMSimpleObjectPooler metalClash01;

        public MMSimpleObjectPooler metalClash02;
        public MMSimpleObjectPooler metalClash03;
        public List<MMSimpleObjectPooler> weaponClashMetalList;

        private void Awake()
        {
            instance = this;
        }

        public void GetRandomBlood(Vector2 location)
        {
            var index = Random.Range(0, bloodSplatterList.Count - 1);
            var hit = bloodSplatterList[index].GetPooledGameObject();
            hit.SetActive(true);
            hit.transform.position = location;
        }

        public void GetRandomGoo(Vector2 location)
        {
            var index = Random.Range(0, gooSplatterList.Count - 1);
            var hit = gooSplatterList[index].GetPooledGameObject();
            hit.SetActive(true);
            hit.transform.position = location;
        }

        public void GetRandomClash(Vector2 location)
        {
            var index = Random.Range(0, weaponClashMetalList.Count - 1);
            var hit = weaponClashMetalList[index].GetPooledGameObject();
            hit.SetActive(true);
            hit.transform.position = location;
        }
    }
}