using System;
using JAssets.Scripts_SC.Items;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class Projectile : Consumable
	{
		public float lifespan = 5f;
		public CircleCollider2D hitbox;
		public int damage = 1;
		public string playerTag;
		private float age = 0f;
		[SerializeField] private GameObject parent;
		

		public Rigidbody2D rb2d;
		private Vector3 lastVelocity;

		private void Update()
		{ 
			age += Time.deltaTime;
			if (age > lifespan) {
				gameObject.SetActive(false);
			}
		}

		private void LateUpdate()
		{
			if (lastVelocity.magnitude > 0.01f) {
				transform.rotation = Quaternion.LookRotation(lastVelocity);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			Debug.Log(playerTag);	
			if (other.GetComponent<PlayerMoveController>().isPlayer && other.CompareTag(playerTag) == false)
			{
				Debug.Log("in the func");
				DealDamageAndSpawnDmgText(other);
			}
			else
			{
				Debug.Log(other.name);
			}
		}
		
		protected void DealDamageAndSpawnDmgText(Collider2D other)
		{
			var otherPlayer = other.gameObject.GetComponentInChildren<PlayerHealthController>();
			otherPlayer.GetDamaged(damage);
			HandleBloodParticle(other);
			var dmgText = DamageNumberController.Instance.player.GetFeedbackOfType<MMF_FloatingText>();
			dmgText.Value = damage.ToString();
			DamageNumberController.Instance.player.PlayFeedbacks(other.transform.position);
		}
		
		private static void HandleBloodParticle(Collider2D other)
		{
			var blood = GetRandomBloodParticle.instance.RandomBloodParticleHandler();
			blood.transform.position = other.transform.position;
			blood.SetActive(true);
		}
	}
}