using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void PauseGame() => Time.timeScale = 0f;
    public void ResumeGame() => Time.timeScale = 1f;
    public void SetNormalSpeed() => Time.timeScale = 1f;
    public void SetFastSpeed() => Time.timeScale = 5f;

    public void SaveProgress()
    {
        // TODO
    }

    public void LoadProgress()
    {
        // TODO
    }
}
