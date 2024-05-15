using JAssets.Scripts_SC.Lists;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class TrackingTrailController : MonoBehaviour
	{
		[SerializeField] private PlayerMoveController controller;
		[SerializeField] private Rigidbody2D rb2d;
		[SerializeField] private Transform groundCheckPoint;
		[SerializeField] private float footPrintTime;
		private float _footPrintTimer;

		private void Start()
		{
			_footPrintTimer = footPrintTime;
		}
	
		private void Update()
		{
			_footPrintTimer -= Time.deltaTime;
			if (_footPrintTimer <= 0)
			{
				_footPrintTimer = footPrintTime;
				CreateFootPrint();
			}
		}

		private void CreateFootPrint()
		{
			var fadingTrail = Library.instance.particleDict["TrailTrace-0"].GetPooledGameObject();
			fadingTrail.transform.position = groundCheckPoint.position;
			fadingTrail.gameObject.SetActive(true);
			fadingTrail.tag = controller.playerTag;
		}	
	}
}
