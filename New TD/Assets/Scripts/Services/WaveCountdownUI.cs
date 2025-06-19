using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class WaveCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private Coroutine countdownCoroutine;

    public void StartCountdown(float seconds, Action onComplete)
    {
        if (countdownCoroutine != null)
            StopCoroutine(countdownCoroutine);

        countdownCoroutine = StartCoroutine(CountdownCoroutine(seconds, onComplete));
    }

    private IEnumerator CountdownCoroutine(float seconds, Action onComplete)
    {
        while (seconds > 0)
        {
            countdownText.text = $"Next wave in {Mathf.CeilToInt(seconds)}...";
            seconds -= Time.deltaTime;
            yield return null;
        }

        countdownText.text = string.Empty;
        onComplete?.Invoke();
    }
}
