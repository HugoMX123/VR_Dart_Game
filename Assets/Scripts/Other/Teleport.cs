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
        playPosition = new Vector3(2.43700004f,0.023f,-2.50900006f);
        waitPosition = new Vector3(5.55000019f,0f,-3.49699998f);
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
