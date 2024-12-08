using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 waitPosition;
    public Vector3 playPosition;
    public CameraFade cameraFade;

    public void Start() 
    {
        cameraFade = FindObjectOfType<CameraFade>();
        playPosition = new Vector3(0.246f,0.042f,-2.38f);
        waitPosition = new Vector3(3.177f,-0.003f,-3.497f);
    }


    // Change the position of this object but before doing so, fade out the screen
    public void TeleportObject(Vector3 newPosition)
    {
        cameraFade.FadeOut();
        StartCoroutine(TeleportCoroutine(newPosition));
    }

    private IEnumerator TeleportCoroutine(Vector3 newPosition)
    {
        yield return new WaitForSeconds(cameraFade.fadeDuration);
        transform.position = newPosition;
        cameraFade.FadeIn();
    }

    public void TeleportToWait()
    {
        TeleportObject(waitPosition);
    }

    public void TeleportToPlay()
    {
        TeleportObject(playPosition);
    }

    
}
