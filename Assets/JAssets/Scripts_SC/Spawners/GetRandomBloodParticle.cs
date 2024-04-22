using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class GetRandomBloodParticle : GetRandomParticle
	{
		public List<MMSimpleObjectPooler> bloodParticlesList = new();
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
