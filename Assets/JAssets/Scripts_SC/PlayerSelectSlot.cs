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
			Debug.Log("in the ready up function");
			DataTypeController.PlayerSlotData slotData = new DataTypeController.PlayerSlotData();
			PlayerSessionData.PlayersReady++;
			isReady = true;
			Debug.Log("2");
			slotData.Color = slotColorImage.color;
			Debug.Log("3");
			slotData.PlayerSlot = slotNumber;
			Debug.Log("4");
			PlayerSessionData.PlayerDataList.Add(slotData);
			slotData.Color = PlayerSessionData.PlayerDataList[slotNumber - 1].Color;
			Debug.Log(PlayerJoinScreen.Instance.teamColors[0]);
			Debug.Log(PlayerSessionData.PlayerDataList[slotNumber - 1].PlayerSlot);
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
