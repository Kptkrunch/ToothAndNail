using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
}
