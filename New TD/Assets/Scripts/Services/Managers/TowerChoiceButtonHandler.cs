using UnityEngine;

public class TowerChoiceButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject towersList;

    private IGameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void OnClick()
    {
        if (towersList == null)
        {
            Debug.LogWarning("TowersList reference is missing.");
            return;
        }

        towersList.SetActive(true);
        gameManager.PauseGame(); 
    }
}
