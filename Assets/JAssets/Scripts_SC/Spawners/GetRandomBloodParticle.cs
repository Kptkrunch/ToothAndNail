using System;
using System.Collections.Generic;
using JAssets.Scripts_SC.Spawners;
using MoreMountains.Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JAssets.Scripts_SC
{
	public class GetRandomBloodParticle : GetRandomParticle
	{
		public static GetRandomBloodParticle instance;
		public List<MMSimpleObjectPooler> bloodParticlesList = new();

		private void Awake()
		{
			instance = this;
		}

		public GameObject RandomBloodParticleHandler()
		{
			if (bloodParticlesList.Count == 0)
			{
				return null;
			}

			int randomIndex = Random.Range(0, bloodParticlesList.Count);
			return bloodParticlesList[randomIndex].GetPooledGameObject();
		}

	}
}
