using UnityEngine;
using System;
using System.Collections;

public class AIDartThrower : MonoBehaviour
{
    public GameObject dartPrefab; // The dart model prefab
    public Transform throwPoint; // The point from which the dart will be thrown
    public float throwForce; // The force applied to the dart when thrown
    // Rotation of the dart when thrown
    public Vector3 throwRotation;
    public Quaternion playerRotation;
    public static event Action OnDartThrown;
    public float throwBoardRadius;
    public int dartCPULevel;

    void Start()
    {
        // Set the throw point to the position of the GameObject's Transform
        throwPoint = this.transform;
    }

    void Update()
    {
        playerRotation = transform.rotation;
        // Check if spacebar is pressed
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartThrowingDarts(dartCPULevel);
        }
    }

    void StartThrowingDarts(int dartCPULevel)
    {
        StartCoroutine(ThrowDartsWithDelay(dartCPULevel));
    }

    // Coroutine to throw darts with a delay between throws
    IEnumerator ThrowDartsWithDelay(int dartCPULevel)
    {
        // First throw
        ThrowDartCPU(dartCPULevel);
        yield return new WaitForSeconds(0.2f); // Wait for 200ms (0.2 seconds)

        // Second throw
        ThrowDartCPU(dartCPULevel);
        yield return new WaitForSeconds(0.2f); // Wait for 200ms

        // Third throw
        ThrowDartCPU(dartCPULevel);
        yield return new WaitForSeconds(0.2f); // Wait for 200ms
    }

    void ThrowDartCPU(int level)
    {
        Quaternion randomRotation = CalculateTargetPosition(level);

        Quaternion currentRotation = transform.rotation;

        Quaternion newRotation = currentRotation * randomRotation;

        Quaternion rotation = Quaternion.Euler(throwRotation);

        GameObject dart = Instantiate(dartPrefab, throwPoint.position, rotation * playerRotation);

        Rigidbody rb = dart.GetComponent<Rigidbody>();
        if (rb != null)
        {
            transform.rotation = newRotation;
            rb.AddForce(throwPoint.forward * throwForce);
            this.transform.rotation = currentRotation;
        }

        OnDartThrown?.Invoke();
    }


    Quaternion CalculateTargetPosition(int level)
    {
        float levelFactor = 0;

        // In level 1, randomize the target position a lot
        if (level == 1)
        {
            levelFactor = 8f;
        }
        
        // In level 2, the target is closer to the center with some randomness
        else if (level == 2)
        {
            levelFactor = 4.5f;
        }
        
        // In level 3, target is very close to the center (almost perfect aim)
        else
        {
            levelFactor = 1.2f;
        }

        // Generate a small random deviation for the rotation (e.g., -5 to 5 degrees)
        float randomRotationX = UnityEngine.Random.Range(-levelFactor, levelFactor); // Slight random rotation on the X-axis
        float randomRotationY = UnityEngine.Random.Range(-levelFactor, levelFactor); // Slight random rotation on the Y-axis
        float randomRotationZ = UnityEngine.Random.Range(-levelFactor, levelFactor); // Slight random rotation on the Z-axis

        // Create a small rotation using Euler angles (degrees)
        Quaternion randomRotation = Quaternion.Euler(randomRotationX, randomRotationY, randomRotationZ);

        return randomRotation;
    }
}
