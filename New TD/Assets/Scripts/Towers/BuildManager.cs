using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    private TowerConfig towerToBuild;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one BuildManager detected!");
            return;
        }
        Instance = this;
    }

    public void SelectTowerToBuild(TowerConfig towerConfig)
    {
        towerToBuild = towerConfig;
    }

    public TowerConfig GetSelectedTower()
    {
        return towerToBuild;
    }

    public void ClearSelection()
    {
        towerToBuild = null;
    }
}
