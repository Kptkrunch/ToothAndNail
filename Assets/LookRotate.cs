using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookRotate : MonoBehaviour
{
    private Vector2 rightStick; // Variable to hold the Vector2 value from the right stick

    [SerializeField] private PolygonCollider2D coneOfView;
    void Update()
    {
        if (rightStick != Vector2.zero)
        {
            float angle = Mathf.Atan2(rightStick.y, rightStick.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<OpponentController>().BeenSpotted(0.25f, "HitEffectBlend", null, null);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<OpponentController>().FadingAway();
        }
    }

    public void OnRightStick(InputAction.CallbackContext context)
    {
        rightStick = context.ReadValue<Vector2>(); // Read the Vector2 value from the right stick
    }
}
