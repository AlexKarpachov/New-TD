using UnityEngine;

public class TowerMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;

    private void OnMouseDown()
    {
        if (menuUI != null)
        {
            menuUI.SetActive(true);
            GameManager.Instance.PauseGame();
        }
    }

    public void CloseMenu()
    {
        if (menuUI != null)
        {
            menuUI.SetActive(false);
            GameManager.Instance.ResumeGame();
        }
    }
}