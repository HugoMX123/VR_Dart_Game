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
    public static event Action OnAIDartThrown;
    public float throwBoardRadius;
    public int dartCPULevel;
    public float delayBetweenThrows = 4f;

    void Start()
    {
        // Set the throw point to the position of the GameObject's Transform
        throwPoint = this.transform;
    }



    public void  StartThrowingDarts()
    {
        StartCoroutine(ThrowDartsWithDelay());
        Debug.Log("AI is throwing darts");
    }

    // Coroutine to throw darts with a delay between throws
    IEnumerator ThrowDartsWithDelay()
    {
        yield return new WaitForSeconds(delayBetweenThrows);
        // First throw
        ThrowDartCPU();
        yield return new WaitForSeconds(delayBetweenThrows); 

        Debug.Log("First dart thrown");

        // Second throw
        ThrowDartCPU();
        yield return new WaitForSeconds(delayBetweenThrows);

        Debug.Log("Second dart thrown"); 

        // Third throw
        ThrowDartCPU();
        yield return new WaitForSeconds(delayBetweenThrows); 

        Debug.Log("Third dart thrown");
    }

    void ThrowDartCPU()
    {
        Quaternion randomRotation = CalculateTargetPosition();

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

         OnAIDartThrown?.Invoke();
       
    }


    Quaternion CalculateTargetPosition()
    {
        float levelFactor = 0;

        // In level 1, randomize the target position a lot
        if (dartCPULevel == 1)
        {
            levelFactor = 8f;
        }
        
        // In level 2, the target is closer to the center with some randomness
        else if (dartCPULevel == 2)
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
