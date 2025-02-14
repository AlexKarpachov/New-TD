using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] Color hoverColor;
    [SerializeField] Vector3 positionOffset;

    private Renderer rend;
    private Color startColor;
    private GameObject tower;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
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
                Debug.Log($"Built {selectedTower.towerName} for {selectedTower.purchaseCost} coins!");
            }
            else
            {
                Debug.Log("Not enough money!");
            }
        }
    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
