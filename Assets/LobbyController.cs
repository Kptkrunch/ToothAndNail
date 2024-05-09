using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
	[SerializeField] private MMF_Player player;

	private void Start()
	{
		player.PlayFeedbacks();
	}
}
