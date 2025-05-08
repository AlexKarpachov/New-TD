using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;

    private void Update()
    {
        if (WaveManager.Instance == null) return;

        int current = Mathf.Clamp(WaveManager.Instance.CurrentWave, 1, WaveManager.Instance.TotalWaves);
        int total = WaveManager.Instance.TotalWaves;

        waveText.text = $"{current}/{total}";
    }
}
