using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Color hoverColor;

    Color invisibleColor = new Color(0, 0, 0, 0);
    Renderer rend;
    GameObject tower;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        HideGround();
    }

    private void OnMouseDown()
    {
        if (tower != null) return;

        TowerConfig selectedTower = BuildManager.Instance.GetSelectedTower();
        if (selectedTower == null) return;

        if (CurrencyManager.Instance.CanAfford(selectedTower.purchaseCost))
        {
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
