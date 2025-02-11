using UnityEngine;

public interface IObjectPool
{
    GameObject GetObject(string objectTag);
    void ReturnObject(GameObject obj, string objectTag);
}
