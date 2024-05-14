using UnityEngine;
using UnityEngine.InputSystem;

namespace JAssets.Scripts_SC
{
	public class GameController : MonoBehaviour
	{
		[SerializeField]
		private GameObject playerPrefab; 

		private void Start()
		{
			for (int i = 0; i < 4; i++)
			{
				var playerInput = PlayerInput.Instantiate(playerPrefab);
            
				int playerIndex = playerInput.playerIndex;

				playerInput.transform.position = GetInitialPlayerPosition(playerIndex);
			}
		}

		Vector3 GetInitialPlayerPosition(int playerIndex)
		{
			return Vector2.zero;
		}
	}
}