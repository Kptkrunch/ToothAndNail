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
			Debug.Log(interactable);
			Debug.Log(interactable.GetComponent<ItemNode>().name);
			Debug.Log(interactable.GetComponent<ItemNode>());
			
			interactable.GetComponent<ItemNode>().ActivateNode();
		}
		
		// Add a OnDrawGizmos method to visualize the overlap circle
		private void OnDrawGizmos() 
		{
			Gizmos.color = Color.red; // Set the color of the gizmo
			Gizmos.DrawWireSphere(transform.position, 1); // Draw a wireframe sphere at the ResourceController's position
		}
	}
}
