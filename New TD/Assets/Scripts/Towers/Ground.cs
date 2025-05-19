using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Color hoverColor;
    [SerializeField] GameObject pauseButtons;
    [SerializeField] GameObject speedButtons;

    Color invisibleColor = new Color(0, 0, 0, 0);
    Renderer rend;
    GameObject tower;

    private IGameManager gameManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        HideGround();
        gameManager = GameManager.Instance;
    }

    private void OnMouseDown()
    {
        if (tower != null) return;

        TowerConfig selectedTower = BuildManager.Instance.GetSelectedTower();
        if (selectedTower == null) return;

        if (CurrencyManager.Instance.CanAfford(selectedTower.purchaseCost))
        {
            gameManager.ResumeGame();
            pauseButtons.SetActive(true);
            speedButtons.SetActive(true);
            CurrencyManager.Instance.SpendMoney(selectedTower.purchaseCost);
            tower = Instantiate(selectedTower.prefab, transform.position, Quaternion.identity);
            BuildManager.Instance.ClearSelection();
            GroundManager.Instance.HideAllGrounds();
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    public void ShowGround()
    {
        if (rend != null)
            rend.material.color = hoverColor;
    }

    public void HideGround()
    {
        if (rend != null)
            rend.material.color = invisibleColor;
    }
}
