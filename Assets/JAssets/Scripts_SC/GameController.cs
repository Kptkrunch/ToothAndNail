using UnityEngine;
using UnityEngine.InputSystem;

namespace JAssets.Scripts_SC
{
	public class GameController : MonoBehaviour
	{
		[SerializeField]
		private GameObject playerPrefab; // set this in Inspector

		private void Start()
		{
			for (int i = 0; i < 4; i++)
			{
				// Instantiate a new PlayerInput for each player
				var playerInput = PlayerInput.Instantiate(playerPrefab);
            
				// Access the playerIndex property
				int playerIndex = playerInput.playerIndex;

				// Use the playerIndex for anything player-specific, like setting initial positions
				playerInput.transform.position = GetInitialPlayerPosition(playerIndex);
			}
		}

		Vector3 GetInitialPlayerPosition(int playerIndex)
		{
			// Return some position based on the player index
			return Vector2.zero;
		}
	}
}