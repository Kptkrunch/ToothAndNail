using JAssets.Scripts_SC.Spawners;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAssets.Scripts_SC
{
	public class PlayerResourceCollector : MonoBehaviour
	{
		private bool canCollect;
		private Collider2D interactible;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Interactable"))
			{
				interactible = other;
				canCollect = true;
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.CompareTag("Interactable"))
			{
				interactible = null;
				canCollect = false;
			}
		}

		public void CollectFromNode(InputAction.CallbackContext context)
		{
			if (!context.performed || interactible == null) return;
			interactible.GetComponent<ItemNode>().ActivateNode();
		}
	}
}
