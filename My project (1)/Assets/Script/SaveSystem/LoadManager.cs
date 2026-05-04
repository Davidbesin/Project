using UnityEngine;
using System;
using System.IO;

public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;

    public event Action AllMineLoad;
    public event Action AllTowerLoad;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadAllMines()
    {
        AllMineLoad?.Invoke();
        LoadAll();
        Debug.Log("All mines loaded.");
    }

    public void LoadAllTowers()
    {
        AllTowerLoad?.Invoke();
        Debug.Log("All towers loaded.");
    }

    

    public void LoadAll()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.json");
        foreach (string path in files)
        {
            string json = File.ReadAllText(path);

            if (json.Contains("\"type\": \"mine\""))
            {
                MineSaveData mineData = JsonUtility.FromJson<MineSaveData>(json);
                SpawnMine(mineData);
            }
            else if (json.Contains("\"type\": \"tower\"") || json.Contains("\"type\": \"attackingTower\""))
            {
                TowerSaveData towerData = JsonUtility.FromJson<TowerSaveData>(json);
                SpawnTower(towerData);
            }
            
        }

        Debug.Log("All mines, towers, and hex tiles loaded.");
    }

    private void SpawnMine(MineSaveData data)
    {
        if (!PrefabRegistry.Instance.prefabsDict.TryGetValue("SampleMine", out GameObject prefab))
        {
            Debug.LogError("SpawnMine: Prefab key 'SampleMine' not found!");
            return;
        }

        GameObject instance = Instantiate(prefab, data.position, data.rotation);
        instance.transform.localScale = data.scale;

        Mine mine = instance.GetComponent<Mine>();
        if (mine == null)
        {
            Debug.LogError("SpawnMine: Mine component missing on prefab!");
            return;
        }

        mine.AssignResource(ResourceRegistry.Instance.GetResource(data.resourceKey));

        // Restore upgrade levels
        //SafeAssignLevel(mine.Upgradeablehealth, data.healthLevel, "Mine Health");
    

        Debug.Log($"SpawnMine: Mine loaded at {data.position} with resourceKey={data.resourceKey}");
    }

    private void SpawnTower(TowerSaveData data)
    {
        if (!PrefabRegistry.Instance.prefabsDict.TryGetValue("AttackTowerBundle", out GameObject prefab))
        {
            Debug.LogError("SpawnTower: Prefab key 'AttackTowerBundle' not found!");
            return;
        }

        GameObject instance = Instantiate(prefab, data.position, data.rotation);
        instance.transform.localScale = data.scale;

        BaseDefensiveTower tower = instance.GetComponent<BaseDefensiveTower>();
        if (tower == null)
        {
            Debug.LogError("SpawnTower: BaseDefensiveTower component missing on prefab!");
            return;
        }

        // Restore upgrade levels
       // SafeAssignLevel(tower.UpgradeableHealth, data.healthLevel, "Tower Health");

        Debug.Log($"SpawnTower: Tower loaded at {data.position} with upgrade levels applied.");
    }

    private void SafeAssignLevel(UpgradeableStatInterface stat, int level, string name)
    {
        if (stat != null)
        {
            stat.AssignLevel(level);
        }
        else
        {
            Debug.LogWarning($"SafeAssignLevel: {name} stat interface is null!");
        }
    }

    
}
