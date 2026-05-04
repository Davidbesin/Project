using System;
using UnityEngine;

[System.Serializable]
public class MineSaveData
{
    public string type;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public int resourceKey; // key for resource type

    public MineSaveData(Mine mine)
    {
        type = "mine";
        position = mine.transform.position;
        rotation = mine.transform.rotation;
        scale = mine.transform.localScale;

        // Use the resource type to get the key
        resourceKey = ResourceRegistry.Instance.GetKey(mine.ResourceType);

        // Safe null checks in case upgradeables aren’t initialized
        
    }
}
