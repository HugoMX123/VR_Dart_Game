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
    private bool canThrow = true;

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
        if(!canThrow)
        {
            return;
        }
        // Convert the throwRotation to a Quaternion
        Quaternion rotation = Quaternion.Euler(throwRotation);

        // Instantiate the dart at the throw point with the correct rotation
        GameObject dart = Instantiate(dartPrefab, throwPoint.position + new Vector3(0, UnityEngine.Random.Range(0.6f, 0.8f), 0), rotation * playerRotation);

        // Add force to the dart's Rigidbody to simulate the throw
        Rigidbody rb = dart.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(throwPoint.forward * throwForce);
        }

        OnDartThrown?.Invoke(); // Event
    }

    public void SetCanThrow(bool value)
    {
        canThrow = value;
    }

    public bool getCantThrow()
    {
        return canThrow;
    }
}
