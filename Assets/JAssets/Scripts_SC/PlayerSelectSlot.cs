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
		public Image slotColorImage; 
		public Button readyButton;
		public Button leftButton;
		public Button rightButton;

		public void ReadyUp()
		{
			DataTypeController.PlayerSlotData slotData = new DataTypeController.PlayerSlotData();
			PlayerSessionData.AddToReadyPlayers();
			isReady = true;
			slotData.Color = slotColorImage.color;
			slotData.PlayerSlot = slotNumber;
			readyButton.interactable = false;
			leftButton.interactable = false;
			rightButton.interactable = false;
			PlayerSessionData.PlayerDataList.Add(slotData);
			slotData.Color = PlayerSessionData.PlayerDataList[slotNumber - 1].Color;
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
