using UnityEngine;

[System.Serializable]
public class ForestSaveData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public ForestSaveData(Transform transform)
    {
        position = transform.position;
        rotation = transform.rotation;
        scale = transform.localScale;
    }
}
