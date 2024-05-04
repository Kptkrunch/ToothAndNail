using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
        public GameObject UiPrefab; // Assign this in inspector
		[SerializeField] private Camera cam;
		[SerializeField] public Canvas uiCanvas;
		
        void Start()
		{
			cam = GetComponentInChildren<Camera>();
			GameObject playerUI = Instantiate(UiPrefab);
			uiCanvas = playerUI.GetComponent<Canvas>();
			uiCanvas.worldCamera = cam;

			// Attach the UI to this player's camera
			playerUI.transform.SetParent(cam.transform, false);
        
			// Position ui in the viewport 
			// If you are using RectTransform in your uiPrefab set the size and position with respect to that.
			// Assuming you have a RectTransform in your playerUI
			RectTransform rect = playerUI.GetComponent<RectTransform>();
			rect.localPosition = Vector3.zero;
			rect.sizeDelta = new Vector2(0, 0);
			rect.localScale = Vector3.one;
			
		}
}
