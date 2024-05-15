using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace JAssets.Scripts_SC
{
	public class MenuButton : MonoBehaviour
	{
		public Button button;
		[SerializeField] private Image buttonImage;
		[SerializeField] private TextMeshProUGUI buttonText;

		public void OpenLocalMp()
		{
			Debug.Log("This");
			SceneManager.LoadScene(1);
		}

		public void OpenOnlineMP()
		{
			SceneManager.LoadScene(2);
		}

		public void OpenOptions()
		{
			// I don't do anything yet
			Debug.Log("a thing");
		}
	}
}
