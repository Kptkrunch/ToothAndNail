using System.Collections.Generic;
using JAssets.Scripts_SC.Multiplayer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace JAssets.Scripts_SC
{
	public class PlayerJoinScreen : MonoBehaviour
	{
		public static PlayerJoinScreen Instance;

		[SerializeField] private List<PlayerSelectSlot> playerSlots;
		[SerializeField] private GameObject startMatchButton;
		public List<Color> teamColors = new();
		public GameObject settingsPanel;
		
		private void Awake()
		{
			if (!Instance) Instance = this;
		}
		
		private void Start()
		{
			FillColorsList();
		}

		private void FixedUpdate()
		{
			startMatchButton.gameObject.SetActive(PlayerSessionData.PlayersReady == PlayerSessionData.TotalPlayers && PlayerSessionData.TotalPlayers > 1);
		}

		public Color GetNewColor(int colorIndex)
		{
			Debug.Log(teamColors[colorIndex]);
			return teamColors[colorIndex];
		}
		
		public void CancelReadyUp()
		{
			foreach (var slot in playerSlots)
			{
				if (slot.deviceId == Gamepad.current.deviceId)
				{
					slot.deviceId = 0;
					slot.gameObject.SetActive(false);
					slot.isReady = false;
					PlayerSessionData.PlayersReady--;
				}
			}
		}
		
		public void ReturnToTitle()
		{
			SceneManager.LoadScene(0);
		}

		public void StartMatch()
		{
			SceneManager.LoadScene(1);
		}
		
		public void OpenSettings()
		{
			if (settingsPanel.activeInHierarchy)
			{
				settingsPanel.SetActive(false);
			}
			else
			{
				settingsPanel.SetActive(true);
			}
		}

		public void PlayerJoin(InputAction.CallbackContext context)
		{
			if (!context.performed) return;
			if (CheckDevicesInUse(Gamepad.current.deviceId)) return;
			playerSlots[PlayerSessionData.TotalPlayers].gameObject.SetActive(true);
			playerSlots[PlayerSessionData.TotalPlayers].deviceId = Gamepad.current.deviceId;
			PlayerSessionData.UpdateTotalPlayers();
			
			Debug.Log(Gamepad.current.deviceId);
		}
		
		private void FillColorsList()
		{
			teamColors.Add(Color.magenta);
			teamColors.Add(Color.blue);
			teamColors.Add(Color.red);
			teamColors.Add(Color.yellow);
			teamColors.Add(Color.white);
			teamColors.Add(Color.green);
		}

		private bool CheckDevicesInUse(int deviceId)
		{
			var deviceExists = false;
			foreach (var t in playerSlots)
			{
				if (t.deviceId == deviceId) deviceExists = true;
			}
			return deviceExists;
		}
	}
}
