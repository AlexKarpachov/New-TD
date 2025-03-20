using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] Color hoverColor;
    [SerializeField] Color invisibleColor = new Color(0, 0, 0, 0);
    [SerializeField] Vector3 positionOffset;

    private Renderer rend;
    private Color startColor;
    private GameObject tower;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        HideGround();
    }

    private void OnMouseDown()
    {
        if (tower != null)
        {
            Debug.Log("Can't build there!");
            return;
        }

        TowerConfig selectedTower = BuildManager.Instance.GetSelectedTower();
        
        if (selectedTower != null)
        {
            if (CurrencyManager.Instance.CanAfford(selectedTower.purchaseCost))
            {
                CurrencyManager.Instance.SpendMoney(selectedTower.purchaseCost);
                tower = Instantiate(selectedTower.prefab, transform.position + positionOffset, Quaternion.identity);
                BuildManager.Instance.ClearSelection();

                GroundManager.Instance.HideAllGrounds();
            }
            else
            {
                Debug.Log("Not enough money!");
            }
        }
    }

    public void ShowGround()
    {
        rend.material.color = hoverColor;
    }

    public void HideGround()
    {
        rend.material.color = invisibleColor;
    }
}
