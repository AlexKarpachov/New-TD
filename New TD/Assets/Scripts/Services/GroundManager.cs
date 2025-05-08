using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public static GroundManager Instance;
    private List<Ground> allGrounds = new List<Ground>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        allGrounds.AddRange(FindObjectsOfType<Ground>());
    }

    public void ShowAllGrounds()
    {
        foreach (Ground ground in allGrounds)
        {
            ground.ShowGround();
        }
    }

    public void HideAllGrounds()
    {
        foreach (Ground ground in allGrounds)
        {
            ground.HideGround();
        }
    }
}
