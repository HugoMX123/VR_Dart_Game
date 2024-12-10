using UnityEngine;  // For MonoBehaviour, Rigidbody, and other Unity-related classes
using System;  // For Action<T> delegate
using System.Collections;  // For IEnumerator and coroutines


public class Dart : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float timeToDestroy;  // Time in seconds before the dart is destroyed

    // Boolean flag to control whether the dart should disappear after a few seconds
    public bool shouldDisappear = false;

    // Rotation speed range (min and max values for random rotation)
    [SerializeField]
    private float minRotationSpeed;  // Minimum rotation speed
    [SerializeField]
    private float maxRotationSpeed;  // Maximum rotation speed

    private float rotationSpeed; // Rotation speed will be randomized
    private bool isRotating = false; // Flag to track whether the dart should rotate

    public static event Action<int, scoreArea> OnDartHit;

    private void Start()
    {
        // Get the Rigidbody component attached to the dart
        rb = GetComponent<Rigidbody>();

        // Set collision detection mode to Continuous for better fast-moving object detection
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Randomly set the rotation speed when the dart is instantiated
        rotationSpeed = UnityEngine.Random.Range(minRotationSpeed, maxRotationSpeed);
        isRotating = true; // Enable rotation immediately
    }

    private void Update()
    {
        // If the dart is frozen (kinematic), and if rotation is enabled, manually rotate it
        if (isRotating)
        {
            // Apply a small rotation around the Z axis
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    { 
            stickTheDart(collision);  

            // If the shouldDisappear flag is true, start the disappearance process
            if (shouldDisappear)
            {
                StartCoroutine(DisappearAfterDelay(timeToDestroy));  // Dart will disappear after a set time
            }
    }

    // Coroutine to make the dart disappear after a delay
    private IEnumerator DisappearAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the dart object
        Destroy(gameObject);
    }

    public void stickTheDart(Collision collision)
    {
        DartMove dm = GetComponent<DartMove>();
        dm.isFlying = false;
        isRotating = false;  // Stop the rotation
        // Check if the dart collided with the dartboard
        if (collision.gameObject.CompareTag("Dartboard"))
        {
            // Stop the dart immediately
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;  // Freeze the dart after the collision. Also helps if its not on the target so it doesnt get the push anymore

        }
        else if (! collision.gameObject.CompareTag("Dart")){
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.AddForce(Vector3.down * 30f, ForceMode.Acceleration);
            //gameManager.suscribeDart(this);
            OnDartHit?.Invoke(0, scoreArea.Zero);
        }
    }
}


