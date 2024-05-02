using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class CameraInitializer : MonoBehaviour
	{
		[SerializeField]
		private Camera cam;
		[SerializeField]
		private GameObject virtualPlayerCam;
 
		void Start()
		{
			PlayerMoveController[] characterMovements = FindObjectsOfType<PlayerMoveController>();
			int layer = characterMovements.Length + 12;

			virtualPlayerCam.layer = layer;
			var bitMask = (1 << layer)
						  | (1 << 0)
						  | (1 << 1)
						  | (1 << 2)
						  | (1 << 4)
						  | (1 << 5)
						  | (1 << 6)
						  | (1 << 7)
						  | (1 << 8)
						  | (1 << 9)
						  | (1 << 10)
						  | (1 << 18)
						  | (1 << 19)
						  | (1 << 21)
						  | (1 << 22)
						  | (1 << 29)
						  | (1 << 30)
						  | (1 << 31);
			
			cam.cullingMask = bitMask;
			cam.gameObject.layer = layer;
		}
	}
}