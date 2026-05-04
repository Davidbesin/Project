using UnityEngine;

[System.Serializable]
public class TowerSaveData
{
    public string type;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;


    // Upgrade levels
    

    public TowerSaveData(BaseDefensiveTower tower)
    {
        type = "attackingTower";
        position = tower.transform.position;
        rotation = tower.transform.rotation;
        scale = tower.transform.localScale;

        // Safe null checks
       
    }
}
