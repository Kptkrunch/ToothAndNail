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

		private void Start()
		{
			spriteRenderer.sharedMaterial = Instantiate(spriteRenderer.sharedMaterial);
		}

		public override void Attack()
		{
			if (isCharging)
			{
				isCharging = false;
				FireArrow();
			}

			if (arrowIsNocked)
			{
				isCharging = true;
				animator.SetBool("ChargeBow", true);
			}
		}

		public override void Special()
		{
			NockArrow();
		}

		private void FireArrow()
		{
			arrowIsNocked = false;
			arrow.SetActive(false);
			animator.SetBool("ChargeBow", false);
			var projectilePrefab = Library.instance.consumableDict["CArrow-0"].GetPooledGameObject();
			if (!projectilePrefab) return;
			
			projectilePrefab.transform.localScale = new Vector3(playerRigidbody2D.transform.localScale.x,
				transform.localScale.y, transform.localScale.z);
			projectilePrefab.SetActive(true);
			projectilePrefab.transform.position = arrow.transform.position;
			projectilePrefab.transform.rotation = arrow.transform.rotation;
			projectilePrefab.GetComponent<Rigidbody2D>().AddForce(new  Vector2(playerRigidbody2D.transform.localScale.x * speed, 5f), ForceMode2D.Impulse);
		}

		private void NockArrow()
		{
			if (arrowIsNocked) return;
			arrowIsNocked = true;
			arrow.SetActive(true);
		}
	}	
}