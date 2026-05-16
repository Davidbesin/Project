using UnityEngine;
using System;
using System.IO;
public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;
    public Transform minesParent;
    public Transform towersParent;

    private void Awake()
    {
        Instance = this;
    }

    private void SpawnMine(MineSaveData data)
    {
        if (!PrefabRegistry.Instance.prefabsDict.TryGetValue("SampleMine", out GameObject prefab))
        {
            Debug.LogError("SpawnMine: Prefab key 'SampleMine' not found!");
            return;
        }

        GameObject instance = Instantiate(prefab, data.position, data.rotation, minesParent);
        instance.transform.localScale = data.scale;

        Mine mine = instance.GetComponent<Mine>();
        if (mine != null)
        {
            mine.AssignResource(ResourceRegistry.Instance.GetResource(data.resourceKey));
        }
    }

    private void SpawnTower(TowerSaveData data)
    {
        if (!PrefabRegistry.Instance.prefabsDict.TryGetValue("AttackTowerBundle", out GameObject prefab))
        {
            Debug.LogError("SpawnTower: Prefab key 'AttackTowerBundle' not found!");
            return;
        }

        GameObject instance = Instantiate(prefab, data.position, data.rotation, towersParent);
        instance.transform.localScale = data.scale;

        BaseDefensiveTower tower = instance.GetComponent<BaseDefensiveTower>();
        if (tower != null)
        {
            // restore upgrades here
        }
    }
}

