using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void RetryLevel()
    {
        GameManager.Instance.ResumeGame();
        FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        GameManager.Instance.ResumeGame();
        FindObjectOfType<SceneFader>().FadeTo("MainMenu"); 
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
