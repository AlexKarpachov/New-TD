using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Vector3 offset;
    private Transform target;

    public void Initialize(Transform target, Vector3 offset)
    {
        this.target = target;
        this.offset = offset;
    }

    public void UpdateHealth(float current, float max)
    {
        fillImage.fillAmount = current / max;
    }

    private void LateUpdate()
    {
        if (target == null) return;
        transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
    }
}