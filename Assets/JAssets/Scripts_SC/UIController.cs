using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class UIController : MonoBehaviour
	{
		public GameObject uiPrefab; 
		[SerializeField] private Camera cam;
		[SerializeField] public Canvas uiCanvas;
		
		void Start()
		{
			cam = GetComponentInChildren<Camera>();
			GameObject playerUI = Instantiate(uiPrefab, cam.transform, false);
			uiCanvas = playerUI.GetComponent<Canvas>();
			uiCanvas.worldCamera = cam;

			// Attach the UI to this player's camera
			RectTransform rect = playerUI.GetComponent<RectTransform>();
			rect.localPosition = Vector3.zero;
			rect.sizeDelta = new Vector2(0, 0);
			rect.localScale = Vector3.one;
			
		}
	}
}
