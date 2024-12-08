using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dartboard : MonoBehaviour
{
    private BoxCollider boxCollider;
    public bool invertAngle = false;  // Boolean flag to invert the angle direction
    public event Action<int, scoreArea> OnHit; // Modified to pass both points and areaHit
    public scoreArea areaHit;
    private int points = 0;

    private void Start()
    {
        // Get the BoxCollider component
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object that collided has a BoxCollider
        if (collision.gameObject.CompareTag("Dart"))
        {
            // Get the first contact point of the collision
            ContactPoint contact = collision.contacts[0];

            // Get the position of the collision in world space
            Vector3 collisionWorldPosition = contact.point;

            // Transform the world position to local space of the collider
            Vector3 localPosition = transform.InverseTransformPoint(collisionWorldPosition);

            // Get the raw radius (distance from center) without normalization
            float r = Mathf.Abs(Mathf.Sqrt(localPosition.x * localPosition.x + localPosition.y * localPosition.y)); // radius is always positive
            float theta = Mathf.Atan2(localPosition.y, localPosition.x); // angle in radians

            // Optionally, invert the angle if the flag is set to true
            if (invertAngle)
            {
                theta += Mathf.PI;  // Add 180 degrees (π radians)
            }

            // Convert theta to degrees for easier interpretation
            float thetaDegrees = theta * Mathf.Rad2Deg; // Convert radians to degrees

            // Ensure positive angle
            if (thetaDegrees < 0)
            {
                thetaDegrees += 360f;  // Ensure the angle is always positive (0 to 360 degrees)
            }

            // Get the DartboardPointSystem component from the Dartboard
            DartboardPointSystem pointSystem = GetComponent<DartboardPointSystem>();

            // Calculate the score and area based on the polar coordinates
            (points, areaHit) = pointSystem.GetScoreFromPolar(thetaDegrees, r); // Tuple unpacking

            // Invoke the OnHit event if there are any listeners
            OnHit?.Invoke(points, areaHit);

            // Debug log the result
            //Debug.Log($"Hit! Points: {points}, Area: {areaHit}");
        }
    }
}


