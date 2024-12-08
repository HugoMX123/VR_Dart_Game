using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraFade : MonoBehaviour
{
    public static CameraFade Instance;
    public Image fadeImage; // Assign your full-screen Image here
    public float fadeDuration;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
        if (fadeImage != null)
        {
            // Ensure the image starts fully transparent
            SetAlpha(0f);
        }
    }


    public void FadeIn()
    {
        StartCoroutine(Fade(1.0f, 0.0f)); // From black to transparent
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(0.0f, 1.0f)); // From transparent to black
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0.0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            SetAlpha(alpha);
            yield return null; // Wait for the next frame
        }

        SetAlpha(endAlpha); // Ensure it ends exactly at the target alpha
    }

    private void SetAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
        }
    }
}

