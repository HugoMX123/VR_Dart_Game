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
    public Player aiPlayer;
    public static event Action OnAIDartThrown;
    public float throwBoardRadius;
    public int dartCPULevel;
    public float delayBetweenThrows = 4f;

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
            StartThrowingDarts();
        }
    }

    public void StartThrowingDarts()
    {
        StartCoroutine(ThrowDartsWithDelay());
    }

    // Coroutine to throw darts with a delay between throws
    IEnumerator ThrowDartsWithDelay()
    {
        yield return new WaitForSeconds(delayBetweenThrows);
        // First throw
        ThrowDartCPU();
        yield return new WaitForSeconds(delayBetweenThrows); 

        int lastThrowScore = aiPlayer.dartScores[aiPlayer.dartScores.Count - 1].score;
        Debug.Log("First dart thrown:" + lastThrowScore);

        if (aiPlayer.dartsLeft > 0) 
        {
            ThrowDartCPU();
            yield return new WaitForSeconds(delayBetweenThrows);

            lastThrowScore = aiPlayer.dartScores[aiPlayer.dartScores.Count - 1].score;
            Debug.Log("Second dart thrown: " + lastThrowScore); 

            if(aiPlayer.dartsLeft > 0) 
            {
                ThrowDartCPU();
                yield return new WaitForSeconds(delayBetweenThrows); 

                lastThrowScore = aiPlayer.dartScores[aiPlayer.dartScores.Count - 1].score;
                Debug.Log("Third dart thrown: " + lastThrowScore);
            }
        }

    }

    void ThrowDartCPU()
    {
        float currentAiScore = aiPlayer.getScore();
        Quaternion doubleOutCorrector = Quaternion.identity;

        if(currentAiScore <= 50)
        {
            dartCPULevel = 3;
        }

        if(currentAiScore <= 40) 
        {
            doubleOutCorrector = getCorrectionAngle();
        }

        Quaternion randomRotation = CalculateTargetPosition();

        Quaternion currentRotation = transform.rotation;

        Quaternion newRotation = currentRotation * randomRotation * doubleOutCorrector;

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

    Quaternion getCorrectionAngle() 
    {
        int currentScore = aiPlayer.getScore();

        Quaternion rotation = Quaternion.identity;

        // Map the score to specific quaternion rotations
        switch (currentScore)
        {
            case 40: //20
                rotation = Quaternion.Euler(-5f, 0f, 0f);
                break;
            case 38: //19
                rotation = Quaternion.Euler(4.9f, -2f, 0f);
                break;
            case 36: //18
                rotation = Quaternion.Euler(-4.5f, 3.5f, 0f);
                break;
            case 34: //17
                rotation = Quaternion.Euler(4.9f, 2f, 0f);
                break;
            case 32: //16
                rotation = Quaternion.Euler(0f, -5f, 0f);
                break;
            case 30: //15
                rotation = Quaternion.Euler(0f, -5f, 0f);
                break;
            case 28: //14
                rotation = Quaternion.Euler(-2f, -4.9f, 0f);
                break;  
            case 26: //13
                rotation = Quaternion.Euler(-2f, 4.9f, 0f);
                break;
            case 24: //12
                rotation = Quaternion.Euler(-4.5f, -3.5f, 0f);
                break;
            case 22: //11
                rotation = Quaternion.Euler(0f, -5f, 0f);
                break;
            case 20: //10
                rotation = Quaternion.Euler(2f, 4.9f, 0f);
                break;
            case 18: //9
                rotation = Quaternion.Euler(0f, -5f, 0f);
                break;      
            case 16: //8
                rotation = Quaternion.Euler(2f, -4.9f, 0f);
                break;  
            case 14: //7
                rotation = Quaternion.Euler(4.5f, -3.5f, 0f);
                break;      
            case 12: //6
                rotation = Quaternion.Euler(0f, 5f, 0f);
                break;    
            case 10: //5
                rotation = Quaternion.Euler(-4.9f, -2f, 0f);
                break;   
            case 8: //4
                rotation = Quaternion.Euler(0f, -5f, 0f);
                break;   
            case 6: //3
                rotation = Quaternion.Euler(5f, 0f, 0f);
                break;                                                                                                                                                                                                       
            case 4: //2
                rotation = Quaternion.Euler(4.5f, 3.5f, 0f);
                break;   
            case 2: //1
                rotation = Quaternion.Euler(-4.9f, 2f, 0f);
                break;                                   
            default: // in case it's odd
                rotation = Quaternion.Euler(-4f, 1.5f, 0f);
                break;
        }

        return rotation;
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
        //Quaternion randomRotation = Quaternion.Euler(0f, 0f, 0f);

        return randomRotation;
    }
}
