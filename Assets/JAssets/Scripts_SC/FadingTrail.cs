using Unity.VisualScripting;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class FadingTrail : MonoBehaviour
	{
		[SerializeField] private float trailFadeTime;
		[SerializeField] private GameObject trailParticle;
		private float trailTimer;
		private bool traceDetected;
		
		private void OnEnable()
		{
			trailTimer = trailFadeTime;
			tag = "Trail";
		}

		private void FixedUpdate()
		{
			trailTimer -= Time.deltaTime;
			if (trailTimer <= 0)
			{
				trailParticle.SetActive(false);
				gameObject.SetActive(false);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(tag) == false && other.GetComponentInParent<LookRotate>())
			{
				Debug.Log("trail triggered it");
				ActivateTrail();
			}
		}

		public void ActivateTrail()
		{
			trailParticle.SetActive(true);
		}
	}
}