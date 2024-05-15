using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace JAssets.Scripts_SC
{
	public class CharacterSelectController : MonoBehaviour
	{
		public static CharacterSelectController Instance;

		[SerializeField] private List<PlayerSelectSlot> playerSlots;
		public List<Sprite> portraitSprites;

		private void Awake()
		{
			if (!Instance)
			{
				Instance = this;
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
			// setting panel set to true
		}
	}
}
