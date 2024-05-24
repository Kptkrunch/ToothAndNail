using JAssets.Scripts_SC.Multiplayer;
using UnityEngine;
using UnityEngine.UI;

namespace JAssets.Scripts_SC
{
	public class PlayerSelectSlot : MonoBehaviour
	{
		public int slotNumber;
		public int colorIndex = 0;
		public int deviceId;
		public bool isReady;
		[SerializeField] private Image slotColorImage;
		[SerializeField] private Button readyButton;
		[SerializeField] private Button leftButton;
		[SerializeField] private Button rightButton;

		public void ReadyUp()
		{
			DataTypeController.PlayerSlotData slotData = new DataTypeController.PlayerSlotData();
			PlayerSessionData.PlayersReady++;
			isReady = true;
			slotData.Color = slotColorImage.color;
			slotData.PlayerSlot = slotNumber;
			PlayerSessionData.PlayerDataList.Add(slotData);
			readyButton.interactable = false;
			slotData.Color = PlayerSessionData.PlayerDataList[slotNumber - 1].Color;
			Debug.Log(PlayerJoinScreen.Instance.teamColors[0]);
			Debug.Log(PlayerSessionData.PlayersReady + " : players ready");
			Debug.Log(PlayerSessionData.PlayerDataList[slotNumber - 1].Color);
		}

		public void ArrowLeft()
		{
			colorIndex--;
			if (colorIndex < 0) colorIndex = 6;
			var newColor = PlayerJoinScreen.Instance.GetNewColor(colorIndex);
			slotColorImage.color = newColor;
		}

		public void ArrowRight()
		{
			colorIndex++;
			if (colorIndex > 6) colorIndex = 0;
			var newColor = PlayerJoinScreen.Instance.GetNewColor(colorIndex);
			slotColorImage.color = newColor;
		}
	}
}
