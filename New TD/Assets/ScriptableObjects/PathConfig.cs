using UnityEngine;

[CreateAssetMenu(fileName = "NewPathConfig", menuName = "Configs/PathConfig")]
public class PathConfig : ScriptableObject
{
    public Vector3[] waypoints; // Список точок шляху.
    public bool isLooping; // Чи повинен ворог йти по шляху циклічно.
}
