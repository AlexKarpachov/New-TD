using UnityEngine;
using UnityEngine.UI;

public class GameSpeedToggle : MonoBehaviour
{
    [SerializeField] private Button fastButton;
    [SerializeField] private Button normalButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playButton;

    private IGameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        fastButton.onClick.AddListener(OnFastSpeed);
        normalButton.onClick.AddListener(OnNormalSpeed);
        pauseButton.onClick.AddListener(OnPause);
        playButton.onClick.AddListener(OnResume);

        SetNormalSpeedOnStart();
    }

    private void SetNormalSpeedOnStart()
    {
        gameManager.SetNormalSpeed();
        UIManager.Instance.ShowSpeedX2Button(true);
        UIManager.Instance.ShowSpeedX1Button(false);
        UIManager.Instance.ShowPauseButton(true);
        UIManager.Instance.ShowPlayButton(false);
    }

    private void OnFastSpeed()
    {
        if (IsPaused()) ResumeFromPause();
        gameManager.SetFastSpeed();
        UIManager.Instance.ShowSpeedX2Button(false);
        UIManager.Instance.ShowSpeedX1Button(true);
    }

    private void OnNormalSpeed()
    {
        if (IsPaused()) ResumeFromPause();
        gameManager.SetNormalSpeed();
        UIManager.Instance.ShowSpeedX2Button(true);
        UIManager.Instance.ShowSpeedX1Button(false);
    }

    private void OnPause()
    {
        gameManager.PauseGame();
        UIManager.Instance.ShowPlayButton(true);
        UIManager.Instance.ShowPauseButton(false);
    }

    private void OnResume()
    {
        gameManager.ResumeGame();
        UIManager.Instance.ShowPauseButton(true);
        UIManager.Instance.ShowPlayButton(false);
    }

    private bool IsPaused() => Time.timeScale == 0f;

    private void ResumeFromPause()
    {
        gameManager.ResumeGame();
        UIManager.Instance.ShowPauseButton(true);
        UIManager.Instance.ShowPlayButton(false);
    }
}
