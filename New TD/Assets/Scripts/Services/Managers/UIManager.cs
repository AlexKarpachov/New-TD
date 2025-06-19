using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject speedX1Button;
    [SerializeField] private GameObject speedX2Button;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowShopPanel(bool show)
    {
        if (shopPanel != null) shopPanel.SetActive(show);
    }

    public void ShowPauseButton(bool show)
    {
        if (pauseButton != null) pauseButton.SetActive(show);
    }

    public void ShowPlayButton(bool show)
    {
        if (playButton != null) playButton.SetActive(show);
    }

    public void ShowSpeedX1Button(bool show)
    {
        if (speedX1Button != null) speedX1Button.SetActive(show);
    }

    public void ShowSpeedX2Button(bool show)
    {
        if (speedX2Button != null) speedX2Button.SetActive(show);
    }
}
