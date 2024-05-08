using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
		SceneManager.LoadScene("JAssets/SettingsItems_SI/Scenes/MovementTestScene");
	}

	public void OpenOnlineMP()
	{
		SceneManager.LoadScene("JAssets/SettingsItems_SI/Scenes/MainMenu");
	}
	
	public void OpenOptions()
	{
		
	}
}
