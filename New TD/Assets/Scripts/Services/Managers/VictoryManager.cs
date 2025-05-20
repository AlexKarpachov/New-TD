using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] private GameObject youWinPanel;

    private bool victoryTriggered = false;

    private void Update()
    {
        if (victoryTriggered) return;

        if (WaveManager.Instance == null || EnemyManager.Instance == null) return;

        bool allWavesCompleted = WaveManager.Instance.AllWavesCompleted;
        bool noEnemiesRemaining = EnemyManager.Instance.GetActiveEnemies().Count == 0;

        if (allWavesCompleted && noEnemiesRemaining)
        {
            TriggerVictory();
        }
    }

    private void TriggerVictory()
    {
        victoryTriggered = true;

        if (youWinPanel != null)
            youWinPanel.SetActive(true);

        GameManager.Instance.PauseGame();
    }
}
