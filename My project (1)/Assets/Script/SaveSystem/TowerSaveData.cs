using UnityEngine;

[System.Serializable]
public class TowerSaveData
{
    public string type;        // identifier for object type
    public int level;          // tower level
    public Vector3 position;   // world position
    public Quaternion rotation;// world rotation
    public Vector3 scale;      // local scale

    public TowerSaveData(BaseDefensiveTower tower, TowerLevel towerLevel, string typeOf)
    {
        type = typeOf;
        position = tower.transform.position;
        rotation = tower.transform.rotation;
        scale = tower.transform.localScale;

        level = (towerLevel != null) ? towerLevel.Level : 1;
    }
}
