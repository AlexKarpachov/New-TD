using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TowerShopUI : MonoBehaviour
{
    [System.Serializable]
    public class TowerData
    {
        public string towerName;
        public int price;
        public Sprite icon;
        public string description;
        public TowerConfig config;
    }

    [Header("References")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject towerEntryPrefab;

    [Header("Towers")]
    [SerializeField] private List<TowerData> towerList;

    private void Start()
    {
        shopPanel.SetActive(false);
        PopulateMenu();
    }

    public void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
        Time.timeScale = shopPanel.activeSelf ? 0 : 1;
    }

    private void PopulateMenu()
    {
        foreach (var tower in towerList)
        {
            GameObject entry = Instantiate(towerEntryPrefab, contentParent);
            entry.transform.Find("Icon").GetComponent<Image>().sprite = tower.icon;
            entry.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = tower.towerName;
            entry.transform.Find("PriceText").GetComponent<TextMeshProUGUI>().text = $"${tower.price}";
            entry.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>().text = tower.description;
            entry.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildManager.Instance.SelectTowerToBuild(tower.config);
                ToggleShop();
            });
        }
    }
}
