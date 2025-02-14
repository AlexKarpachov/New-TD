using UnityEngine;

public class Tower1 : TowerBase
{
    public override void Attack()
    {
        Debug.Log($"{gameObject.name} (Tower1) is shooting at {target.name}!");
    }
}
