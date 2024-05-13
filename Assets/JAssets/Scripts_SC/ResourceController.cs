using JAssets.Scripts_SC.Spawners;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAssets.Scripts_SC
{
	public class ResourceController : MonoBehaviour
	{
		public LayerMask layerMask;

		public void InteractWithObject(InputAction.CallbackContext context)
		{
			if (!context.performed) return;
			var interactable = Physics2D.OverlapCircle(transform.position, 1f, layerMask);
			if (!interactable) return;
			
			interactable.GetComponent<ItemNode>().ActivateNode();
		}
		
		private void OnDrawGizmos() 
		{
			Gizmos.color = Color.red; 
			Gizmos.DrawWireSphere(transform.position, 1);
		}
	}
}
