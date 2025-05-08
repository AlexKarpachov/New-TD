using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages scene transitions with a smooth fade effect.
/// </summary>
public class SceneFader : MonoBehaviour
{
    [SerializeField] private Image img; // The UI Image used for fading
    [SerializeField] private AnimationCurve curve; // Defines the fade animation
    [SerializeField] private float fadeDuration = 0.8f; // Duration of fade effect

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Prevents the fader from being destroyed between scenes
    }

    private void Start()
    {
        StartCoroutine(Fade(1, 0)); // Start scene with fade-in effect
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOutAndLoad(scene));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, curve.Evaluate(t / fadeDuration));
            img.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
        img.color = new Color(0f, 0f, 0f, endAlpha);
    }

    private IEnumerator FadeOutAndLoad(string scene)
    {
        yield return StartCoroutine(Fade(0, 1)); // Fade to black
        SceneManager.LoadScene(scene);
        yield return StartCoroutine(Fade(1, 0)); // Fade in after loading
    }
}
