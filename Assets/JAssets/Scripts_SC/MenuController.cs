using System;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JAssets.Scripts_SC
{
	public class MenuController : MonoBehaviour
	{
		[SerializeField] private Image backgroundImage;
		[SerializeField] private TextMeshProUGUI titleText;
		[SerializeField] private TextMeshProUGUI localMpText;
		[SerializeField] private TextMeshProUGUI onlineMpText;
		[SerializeField] private TextMeshProUGUI optionsText;

		[SerializeField] private Button localMpButton;
		[SerializeField] private Button onlineMpButton;
		[SerializeField] private Button optionsButton;

		[SerializeField] private MMF_Player player;
		private void Start()
		{
			player.PlayFeedbacks();
		}
	}
}
