using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class GetRandomParticle : MonoBehaviour
{
	 private List<MMSimpleObjectPooler> bloodParticlesList = new();
	 
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
