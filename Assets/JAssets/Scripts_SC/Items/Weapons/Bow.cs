using System;
using JAssets.Scripts_SC.Lists;
using UnityEngine;

namespace JAssets.Scripts_SC.Items.Weapons
{
	public class Bow : Weapon
	{
		public SpriteRenderer spriteRenderer;
		public Rigidbody2D playerRigidbody2D;
		public GameObject arrow;

		public bool isCharging; 
		public bool arrowIsNocked;
		public float speed = 7;
		public float chargeLevel;
		public float maxChargeLevel = 2f;
		private static readonly int ChargeBow = Animator.StringToHash("ChargeBow");
		private static readonly int Glow = Shader.PropertyToID("_Glow");
		private static readonly int Fire = Animator.StringToHash("Fire");
		private static readonly int Nock = Animator.StringToHash("Nock");

		private void Start()
		{
			spriteRenderer.sharedMaterial = Instantiate(spriteRenderer.sharedMaterial);
		}

		private void FixedUpdate()
		{
			MaybeChargeBow();
		}
		
		private void OnDisable()
		{
			ResetBow();
		}

		public override void Attack()
		{
			if (!arrowIsNocked || !isCharging) return;
			animator.SetBool(Fire, true);
			var projectilePrefab = Library.instance.consumableDict["CArrow-0"].GetPooledGameObject();
			if (!projectilePrefab) return;

			projectilePrefab.transform.localScale = new Vector3(playerRigidbody2D.transform.localScale.x,
				transform.localScale.y, transform.localScale.z);
			projectilePrefab.SetActive(true);
			projectilePrefab.transform.position = arrow.transform.position;
			projectilePrefab.transform.rotation = arrow.transform.rotation;
			projectilePrefab.GetComponent<Rigidbody2D>()
				.AddForce(new Vector2(playerRigidbody2D.transform.localScale.x * speed * chargeLevel, 5f),
					ForceMode2D.Impulse);
			
			ResetBow();
		}

		public override void Special()
		{
			if (arrowIsNocked) isCharging = true;
		}

		private void ResetBow()
		{
			spriteRenderer.sharedMaterial.SetFloat(Glow, 0);
			chargeLevel = 0;
			arrow.SetActive(false);
			arrowIsNocked = false;
			isCharging = false;
			animator.SetBool(ChargeBow, false);
			animator.SetBool(Fire, false);
			animator.SetBool(Nock, false);
		}

		private void NockArrow()
		{
			if (arrowIsNocked)
			{
				Debug.Log("arrow is knocked: " + arrowIsNocked);
				isCharging = true;
			}
			if (arrowIsNocked) return;
			arrowIsNocked = true;
			arrow.SetActive(true);
		}
		
		private void MaybeChargeBow()
		{
			if (isCharging)
			{
				spriteRenderer.sharedMaterial.SetFloat(Glow, chargeLevel);
				chargeLevel += Time.deltaTime;
			} 
			
			if (chargeLevel >= maxChargeLevel) Attack();
		}
	}	
}