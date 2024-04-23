using System;
using JAssets.Scripts_SC.Lists;
using JAssets.Scripts_SC.SOScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAssets.Scripts_SC.Items.Weapons
{
	public class Bow : Weapon
	{
		public GameObject arrowPrefab; // reference to the arrow prefab
		public Transform animationEndTransform; // reference to the animation end position and rotation
		private bool isCharging;
		private bool arrowIsNocked;
		private float chargeStartTime;
		public float maxForce = 500f;  // Maximum force when charged fully
		public float maxChargeTime = 2f; // Maximum time for charge (in seconds)
		public float chargeDuration;
		public float chargeRatio;
		public float force;
		public Weapon_SO rtso;

		private void Start()
		{
			rtso = Instantiate(rtso);
		}

		private void Update()
		{
			if (!(Time.time > chargeDuration || !isCharging)) return;
			arrowIsNocked = false;
			chargeRatio = Mathf.Clamp01(chargeDuration / maxChargeTime);
			force = maxForce * chargeRatio;
			isCharging = false;
			Attack();
		}
		public override void Special()
		{
			NockArrow();
		}

		public void Attack()
		{
			var arrow = Library.instance.consumableDict["arrow"].GetPooledGameObject();
			var arrowRigidbody = arrow.GetComponent<Rigidbody>();

			if (arrowRigidbody)
			{
				arrow.GetComponent<Projectile>().damage = rtso.damage;
				arrow.transform.position = animationEndTransform.position;
				arrow.transform.rotation = animationEndTransform.rotation;
				arrowRigidbody.AddForce(arrow.transform.forward * force);
			}
			else
			{
				Debug.LogError("Arrow prefab does not have Rigidbody component");
			}
		}

		public void ChargeBow(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				// pull back animation
				isCharging = true;
				chargeStartTime = Time.time;
			}
			else if (context.canceled)
			{
				// release animation
				isCharging = false;
				chargeDuration = Time.time - chargeStartTime;
				arrowIsNocked = false;
				chargeRatio = Mathf.Clamp01(chargeDuration / maxChargeTime);
				force = maxForce * chargeRatio;
			}
		}
		private void NockArrow()
		{
			arrowIsNocked = true;
			// set animation
		}
	}
}