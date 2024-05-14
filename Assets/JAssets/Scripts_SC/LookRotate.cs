using UnityEngine;
using UnityEngine.InputSystem;

namespace JAssets.Scripts_SC
{
    public class LookRotate : MonoBehaviour
    {
        private Vector2 rightStick;
        [SerializeField] private PolygonCollider2D coneOfView;
        private void Update()
        {
            if (rightStick != Vector2.zero)
            {
                float angle = Mathf.Atan2(rightStick.y, rightStick.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Trail"))
            {
                Debug.Log("cone spotted it");
                other.GetComponent<FadingTrail>().ActivateTrail();
            }
        }

        public void OnRightStick(InputAction.CallbackContext context)
        {
            rightStick = context.ReadValue<Vector2>(); // Read the Vector2 value from the right stick
        }
    }
}
