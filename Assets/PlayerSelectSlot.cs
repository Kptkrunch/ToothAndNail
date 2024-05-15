using JAssets.Scripts_SC;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectSlot : MonoBehaviour
{
	[SerializeField] private Image portraitImage;
	private int _portraitIndex = 0;
	[SerializeField] private int readyPlayers = 0;
	[SerializeField] private Button portraitButton;
	[SerializeField] private Button readyButton;
	[SerializeField] private Button leftButton;
	[SerializeField] private Button rightButton;

	public void ActivatePlayerSelectPortrait()
	{
		leftButton.gameObject.SetActive(true);
		rightButton.gameObject.SetActive(true);
		// highlight the frame around the window
		// on second press deactivate and allow for movement to bottom area
	}
	
	public void ReadUp()
	{
		readyPlayers += 1;
		Debug.Log("Ready: " + readyPlayers);
		// grey out the player select slot
	}
	
	public void CancelReadyUp()
	{
		readyPlayers -= 1;
		Debug.Log("Ready: " + readyPlayers);
		// reactivate character select slot
	}

	public void ArrowLeft()
	{
		_portraitIndex = (_portraitIndex - 1 + CharacterSelectController.Instance.portraitSprites.Count) %
						CharacterSelectController.Instance.portraitSprites.Count;
		UpdatePortraitSprite();
		Debug.Log("left: " + _portraitIndex);
	}

	public void ArrowRight()
	{
		_portraitIndex = (_portraitIndex + 1) % CharacterSelectController.Instance.portraitSprites.Count;
		UpdatePortraitSprite();
		Debug.Log("right: " + _portraitIndex);
	}

	private void UpdatePortraitSprite()
	{
		portraitImage.sprite = CharacterSelectController.Instance.portraitSprites[_portraitIndex];
	}

	private void LockInPortrait()
	{
		// lock in the portrait
	}
}
