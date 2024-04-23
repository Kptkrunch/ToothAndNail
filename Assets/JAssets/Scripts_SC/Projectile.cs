using System;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class Projectile : MonoBehaviour
	{
		public float lifespan = 10f;
		public int damage = 1;
		private float age = 0f;
		

		private Rigidbody rb2d;
		private Vector3 lastVelocity;

		private void Awake()
		{
			rb2d = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			lastVelocity = rb2d.velocity;

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
				// play breaking animation
			}
		}
	}
}