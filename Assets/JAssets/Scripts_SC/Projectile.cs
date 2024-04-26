using System;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class Projectile : MonoBehaviour
	{
		public float lifespan = 10f;
		public CircleCollider2D hitbox;
		public int damage = 1;
		private float age = 0f;
		

		public Rigidbody2D rb2d;
		private Vector3 lastVelocity;

		private void Start()
		{
		}

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

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				other.gameObject.GetComponent<PlayerHealthController>().GetDamaged(damage);
				gameObject.SetActive(false);
			}
		}
	}
}