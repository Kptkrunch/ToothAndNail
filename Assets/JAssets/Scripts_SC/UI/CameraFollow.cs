using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public float smoothness = 5.0f;

	private Vector3 offset;

	void Start ()
	{
		// check if target is assigned, else log warning
		if (target == null)
		{
			Debug.LogWarning("Cannot find target!");
		}
		else
		{
			// calculate initial offset between camera and target
			offset = transform.position - target.position;
		}
	}

	void LateUpdate ()
	{
		if (target != null)
		{
			// determine the new position of the camera
			Vector3 desiredPosition = target.position + offset;
        
			// smooth the transition between the current and desired positions
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothness * Time.deltaTime);

			// update the position
			transform.position = smoothedPosition;
		}
	}
}