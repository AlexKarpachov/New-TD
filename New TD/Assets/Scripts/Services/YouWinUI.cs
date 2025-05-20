using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWinUI : MonoBehaviour
{
    private SceneFader sceneFader;
    private bool isTransitioning = false;

    private void Awake()
    {
        sceneFader = FindObjectOfType<SceneFader>();
        if (sceneFader == null)
        {
            Debug.LogError("SceneFader not found in the scene!");
        }
    }

    public void RetryGame()
    {
        if (isTransitioning || sceneFader == null) return;

        isTransitioning = true;
        GameManager.Instance.ResumeGame();
        sceneFader.FadeTo("Level1");
    }

    public void LoadMainMenu()
    {
        if (isTransitioning || sceneFader == null) return;

        isTransitioning = true;
        GameManager.Instance.ResumeGame();
        sceneFader.FadeTo("MainMenu");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
