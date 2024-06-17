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
			if (PlayerSessionData.TotalPlayers == PlayerSessionData.PlayersReady && PlayerSessionData.TotalPlayers > 1)
			{
				startMatchButton.SetActive(true);
			}
			else
			{
				startMatchButton.SetActive(false);
			}
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
					slot.GetComponentInChildren<PlayerSelectSlot>().readyButton.interactable = true;
					slot.GetComponentInChildren<PlayerSelectSlot>().leftButton.interactable = true;
					slot.GetComponentInChildren<PlayerSelectSlot>().rightButton.interactable = true;
					slot.isReady = false;
					PlayerSessionData.RemoveFromReadyPlayers();
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
			Debug.Log("Loaded Scene");
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
			PlayerSessionData.AddToTotalPlayers();
			Debug.Log(PlayerSessionData.TotalPlayers + " : total players");
			Debug.Log(Gamepad.current.deviceId);
		}

		public void PlayerLeaveMatch(InputAction.CallbackContext context)
		{
			if (!context.performed) return;
			foreach (var slot in playerSlots)
			{
				if (slot.deviceId == Gamepad.current.deviceId)
				{
					PlayerSessionData.RemoveFromTotalPlayers();
					slot.colorIndex = 0;
					slot.slotColorImage.color = GetNewColor(0);
					slot.gameObject.SetActive(false);
				}
			}
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
