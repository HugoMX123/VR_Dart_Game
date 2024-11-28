using UnityEngine;
using System;

public class DartThrower : MonoBehaviour
{
    public GameObject dartPrefab; // The dart model prefab
    public Transform throwPoint; // The point from which the dart will be thrown
    public float throwForce; // The force applied to the dart when thrown
    // Rotation of the dart when thrown
    public Vector3 throwRotation;
    public Quaternion playerRotation;
    public static event Action OnDartThrown;

    void Start()
    {
        // Set the throw point to the position of the GameObject's Transform
        throwPoint = transform.GetChild(0).GetComponent<Transform>();
    }

    void Update()
    {
        playerRotation = transform.rotation;
        // Check if spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowDart();
        }
    }

    void ThrowDart()
    {
        // Convert the throwRotation to a Quaternion
        Quaternion rotation = Quaternion.Euler(throwRotation);

        // Instantiate the dart at the throw point with the correct rotation
        GameObject dart = Instantiate(dartPrefab, throwPoint.position, rotation * playerRotation);

        // Add force to the dart's Rigidbody to simulate the throw
        Rigidbody rb = dart.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(throwPoint.forward * throwForce);
        }

        OnDartThrown?.Invoke(); // Event
    }
}
