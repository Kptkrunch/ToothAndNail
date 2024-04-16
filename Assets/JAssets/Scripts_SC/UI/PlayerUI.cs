using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace JAssets.Scripts_SC.UI
{
	public class PlayerUI : MonoBehaviour
	{
		private int _maxHealth = 100;
		private int _currentHealth = 100;

		public MMProgressBar healthBar;
		public Image decreaseBar;
		public Image increaseBar;
		public Image foregroundBar;

		public GameObject barChunk;

		public Image weaponImage;
		public Image toolImage;
		public Image craftableImage;
		public void SetBarValues(int maxHealth)
		{
			this._maxHealth = maxHealth;
			_currentHealth = this._maxHealth;
		}

		public void TakeDamage(int damage)
		{
			_currentHealth -= damage;
			_currentHealth = Mathf.Max(0, _currentHealth);
		}
		public void HealDamage(int healAmount)
		{
			_currentHealth -= healAmount;
			_currentHealth = Mathf.Min(_maxHealth, _currentHealth);
		}

		public void UpdateItemsUi(string itemType, string itemSlot, Sprite sprite)
		{
			Debug.Log(itemType);
			Debug.Log(itemSlot);
			switch (itemType)
			{
				case "Weapon":
					weaponImage.sprite = sprite;
					Debug.Log(sprite.name);
					Debug.Log(weaponImage.sprite.name);
					break;
				case "Tool":
					toolImage.sprite = sprite;
					break;
				case "Consumable":
					switch (itemSlot)
					{
						case "Weapon":
							weaponImage.sprite = sprite;
							break;
						case "Tool":
							toolImage.sprite = sprite;
							break;
						default:
							craftableImage.sprite = sprite;
							break;
					}
					break;
			}

		}
		
		public void UpdateCraftable(Sprite sprite)
		{
			craftableImage.sprite = sprite;
		}
	}
}