using System.Collections;
using UnityEngine;

public class CastleHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private CastleHealthBar healthBarUI;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (healthBarUI != null)
            healthBarUI.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (healthBarUI != null)
            healthBarUI.UpdateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public int GetCurrentHealth() => currentHealth;

    private void GameOver()
    {
        GameManager.Instance.PauseGame();
        StartCoroutine(FadeGameOverUI());
    }

    private IEnumerator FadeGameOverUI()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            CanvasGroup canvasGroup = gameOverPanel.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = gameOverPanel.AddComponent<CanvasGroup>();

            canvasGroup.alpha = 0f;
            float duration = 0.5f;
            float time = 0f;

            while (time < duration)
            {
                time += Time.unscaledDeltaTime;
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, time / duration);
                yield return null;
            }

            canvasGroup.alpha = 1f;
        }
    }
}
