/*
 * This may be deleted. Replaced with GameSpeedToggle
 * 
 * using UnityEngine;
using UnityEngine.UI;

public class GamePauseToggle : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject towerList;

    private IGameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        pauseButton.onClick.AddListener(OnPause);
        playButton.onClick.AddListener(OnPlay);

        UIManager.Instance.ShowPauseButton(true);
        UIManager.Instance.ShowPlayButton(false);
    }

    private void OnPause()
    {
        gameManager.PauseGame();
        UIManager.Instance.ShowPauseButton(false);
        UIManager.Instance.ShowPlayButton(true);
    }

    private void OnPlay()
    {
        towerList.SetActive(false);
        gameManager.ResumeGame();
        UIManager.Instance.ShowPauseButton(true);
        UIManager.Instance.ShowPlayButton(false);
    }
}*/
