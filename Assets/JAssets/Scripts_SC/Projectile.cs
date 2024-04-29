using JAssets.Scripts_SC.Items;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class Projectile : Consumable
	{
		// public float lifespan = 10f;
		public CircleCollider2D hitbox;
		public int damage = 1;
		private float age = 0f;
		

		public Rigidbody2D rb2d;
		private Vector3 lastVelocity;

		// private void Update()
		// { 
		// 	age += Time.deltaTime;
		// 	if (age > lifespan) {
		// 		gameObject.SetActive(false);
		// 	}
		// }

		private void LateUpdate()
		{
			if (lastVelocity.magnitude > 0.01f) {
				transform.rotation = Quaternion.LookRotation(lastVelocity);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				DealDamageAndSpawnDmgText(other);
			}
		}
		
		protected void DealDamageAndSpawnDmgText(Collider2D other)
		{
			var otherPlayer = other.gameObject.GetComponentInChildren<PlayerHealthController>();
			otherPlayer.GetDamaged(damage);

			var dmgText = DamageNumberController.instance.player.GetFeedbackOfType<MMF_FloatingText>();
			dmgText.Value = damage.ToString();
			DamageNumberController.instance.player.PlayFeedbacks(other.transform.position);
		}
	}
}