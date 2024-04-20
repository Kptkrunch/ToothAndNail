using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class CameraFollow : MonoBehaviour
	{
		public Transform playerTransform; // the player's Transform
		public float smoothSpeed = 0.125f; // set this to adjust the follow speed
		public Vector3 offset; // set this to change the camera position relative to the player

		private void LateUpdate()
		{

			// Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
			// transform.position = smoothedPosition;
		}
	}
}