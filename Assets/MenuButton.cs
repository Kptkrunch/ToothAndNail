using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class MenuButton : MonoBehaviour
{
	public Button button;
	[SerializeField] private Image buttonImage;
	[SerializeField] private TextMeshProUGUI buttonText;

	public void OpenLocalMp()
	{
		Debug.Log("This");
		// do a thing
	}

	public void OpenOnlineMP()
	{
		// do a thing
	}
	
	public void OpenOptions()
	{
		// do a thing
	}
}
