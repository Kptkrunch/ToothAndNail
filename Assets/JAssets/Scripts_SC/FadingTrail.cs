using Unity.VisualScripting;
using UnityEngine;

namespace JAssets.Scripts_SC
{
	public class FadingTrail : MonoBehaviour
	{
		[SerializeField] private float trailFadeTime;
		[SerializeField] private GameObject trailParticle;
		private float _trailTimer;
		private bool _traceDetected;
		
		private void OnEnable()
		{
			_trailTimer = trailFadeTime;
			tag = "Trail";
		}

		private void FixedUpdate()
		{
			_trailTimer -= Time.deltaTime;
			if (_trailTimer <= 0)
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
