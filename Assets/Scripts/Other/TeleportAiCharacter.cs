using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAiCharacter : MonoBehaviour
{
    public Vector3 waitPosition;
    public Vector3 playPosition;

    public Vector3 waitRotation;
    public Vector3 playRotation;

    public void Start() 
    {
        playPosition = new Vector3(2.58f,0f,-2.42f);
        waitPosition = new Vector3(5.577f,0,-3.497f);

        playRotation = new Vector3(0f,170,0f);
        waitRotation = new Vector3(0f,225f,0f);
    }

    public void TeleportToPlay()
    {
        transform.position = playPosition;
        transform.eulerAngles = playRotation;
    }

    public void TeleportToWait()
    {
        transform.position = waitPosition;
        transform.eulerAngles = waitRotation;
    }


}
