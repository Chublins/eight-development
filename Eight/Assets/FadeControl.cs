using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance;
    public Image fadeImage;
    public float fadeDuration = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // Ensure the fade image is fully transparent initially
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeOut(System.Action onComplete = null)
    {
        StartCoroutine(Fade(1, onComplete));
    }

    public void FadeIn(System.Action onComplete = null)
    {
        StartCoroutine(Fade(0, onComplete));
    }

    IEnumerator Fade(float targetAlpha, System.Action onComplete)
    {
        float startAlpha = fadeImage.color.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlpha);
            yield return null;
        }

        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
        onComplete?.Invoke();
    }
}
