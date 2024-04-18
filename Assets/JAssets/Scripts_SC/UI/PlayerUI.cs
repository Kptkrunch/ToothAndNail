using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace JAssets.Scripts_SC.UI
{
	public class PlayerUI : MonoBehaviour
	{
		private int _maxHealth = 100;
		private int _currentHealth = 100;

		[SerializeField] private Sprite emptyHand;

		public MMProgressBar healthBar;
		public Image decreaseBar;
		public Image increaseBar;
		public Image foregroundBar;

		public GameObject barChunk;

		public Image imageSlotA;
		public Image imageSlotB;
		public Image recipteImage;
		public void SetBarValues(int maxHealth)
		{
			_maxHealth = maxHealth;
			_currentHealth = _maxHealth;
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
		public void UpdateItemsUi(string imageSlot, Sprite sprite)
		{
			switch (imageSlot)
			{
				case "A":
					imageSlotA.sprite = sprite;
					break;
				case "B":
					imageSlotB.sprite = sprite;
					break;
				case "C":
					recipteImage.sprite = sprite;
					break;
			}
		}

		public void ClearImagesUi()
		{
			imageSlotA.sprite = null;
			imageSlotB.sprite = null;
			recipteImage.sprite = null;
		}
	}
}