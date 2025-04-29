using UnityEngine;
using UnityEngine.UI;

public class GameSpeedToggle : MonoBehaviour
{
    [SerializeField] private Button fastButton;    // Button "x2"
    [SerializeField] private Button normalButton;  // Button "x1"
    [SerializeField] private Button pauseButton;   // Button "Pause"
    [SerializeField] private Button playButton;    // Button "Play"

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
        ShowFastButton(); // показати кнопку x2 на старті
        ShowPauseButton(); // на старті також активна кнопка пауза
    }

    private void OnFastSpeed()
    {
        if (IsPaused()) ResumeFromPause();
        gameManager.SetFastSpeed();
        ShowNormalButton();
    }

    private void OnNormalSpeed()
    {
        if (IsPaused()) ResumeFromPause();
        gameManager.SetNormalSpeed();
        ShowFastButton();
    }

    private void OnPause()
    {
        gameManager.PauseGame();
        ShowPlayButton();
    }

    private void OnResume()
    {
        gameManager.ResumeGame();
        ShowPauseButton();
    }

    private void ShowFastButton()
    {
        fastButton.gameObject.SetActive(true);
        normalButton.gameObject.SetActive(false);
    }

    private void ShowNormalButton()
    {
        fastButton.gameObject.SetActive(false);
        normalButton.gameObject.SetActive(true);
    }

    private void ShowPauseButton()
    {
        pauseButton.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
    }

    private void ShowPlayButton()
    {
        pauseButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
    }

    private bool IsPaused()
    {
        return Time.timeScale == 0f;
    }

    private void ResumeFromPause()
    {
        gameManager.ResumeGame();
        ShowPauseButton();
    }
}
