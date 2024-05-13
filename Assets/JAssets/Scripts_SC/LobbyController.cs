using MoreMountains.Feedbacks;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class LobbyController : MonoBehaviour
	{
		[SerializeField] private MMF_Player player;

		private void Start()
		{
			player.PlayFeedbacks();
		}
	}
}
