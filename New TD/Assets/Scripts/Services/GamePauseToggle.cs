using UnityEngine;
using UnityEngine.UI;

public class GamePauseToggle : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playButton;

    private IGameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        pauseButton.onClick.AddListener(OnPause);
        playButton.onClick.AddListener(OnPlay);

        pauseButton.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
    }

    private void OnPause()
    {
        gameManager.PauseGame();
        pauseButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
    }

    private void OnPlay()
    {
        gameManager.ResumeGame();
        pauseButton.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
    }
}
