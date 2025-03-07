using UnityEngine;

/// <summary>
/// This class is unnecessary and can be deleted because it inherits from TowerBase where the whole tower logic processes. 
/// But if in future there is a need to add unique features to every or some towers - it's better to do it in Tower1 (Tower2, etc.) class
/// </summary>
public class Tower1 : TowerBase
{
    public override void Attack(Transform target)
    {
        Debug.Log($"{gameObject.name} (Tower1) is shooting at {target.name}!");
    }
}
