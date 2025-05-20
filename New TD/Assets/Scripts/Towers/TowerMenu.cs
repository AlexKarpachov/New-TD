using UnityEngine;

public class TowerMenu : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main == null) return;

        Vector3 camDirection = transform.position - Camera.main.transform.position;
        transform.rotation = Quaternion.LookRotation(camDirection);
    }
}
