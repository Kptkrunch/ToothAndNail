using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace JAssets.Scripts_SC.Spawners
{
	public class GetRandomParticle : MonoBehaviour
	{
		private readonly List<MMSimpleObjectPooler> bloodParticlesList = new();
	 
		public GameObject RandomParticleHandler()
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
