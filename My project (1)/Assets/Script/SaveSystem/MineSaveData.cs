using System;
using UnityEngine;

[System.Serializable]
public class MineSaveData
{
    public string type;
    public int level;
    public int resourceKey; // key for resource type
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public MineSaveData(Mine mine, MineLevel mineLevel, string typeOf)
    {
        type = typeOf;
        position = mine.transform.position;
        rotation = mine.transform.rotation;
        scale = mine.transform.localScale;

        // Use the resource type to get the key
       resourceKey = (mine.ResourceType != null) 
            ? ResourceRegistry.Instance.GetKey(mine.ResourceType) 
            : -1;

        // Safe null checks in case upgradeables aren’t initialized
         level = (mineLevel != null) ? mineLevel.Level : 0;
    }
}
