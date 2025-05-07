using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls enemy status bars (health and armor) in world space.
/// Keeps it facing the camera and updates values.
/// </summary>
public class EnemyStatusBar : MonoBehaviour
{
    [SerializeField] Image healthFillImage;
    [SerializeField] Image armorFillImage;
    [SerializeField] Vector3 offset;
    public Vector3 Offset => offset;

    private Transform target;

    public void Initialize(Transform target, Vector3 offset)
    {
        this.target = target;
        this.offset = offset;
    }

    public void UpdateHealth(float current, float max)
    {
        healthFillImage.fillAmount = Mathf.Clamp01(current / max);
    }

    public void UpdateArmor(float current, float max)
    {
        armorFillImage.fillAmount = Mathf.Clamp01(current / max);
    }

    private void LateUpdate()
    {
        if (target == null || Camera.main == null) return;

        transform.position = target.position + offset;
        Vector3 camDirection = transform.position - Camera.main.transform.position;
        transform.rotation = Quaternion.LookRotation(camDirection);
    }
}
