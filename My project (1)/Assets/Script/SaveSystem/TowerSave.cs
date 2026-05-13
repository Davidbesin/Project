using UnityEngine;
using System.IO;

[RequireComponent(typeof(BaseDefensiveTower))]
[RequireComponent(typeof(TowerLevel))]
public class TowerSave : MonoBehaviour
{
    private BaseDefensiveTower tower;
    private TowerLevel towerLevel;

    [SerializeField] string typeOf;

    private void Awake()
    {
        tower = GetComponent<BaseDefensiveTower>();
        towerLevel = GetComponent<TowerLevel>();
    }

    public void SaveTower()
    {
        if (tower == null || towerLevel == null)
        {
            Debug.LogError("Tower or TowerLevel missing!");
            return;
        }

        TowerSaveData data = new TowerSaveData(tower, towerLevel, typeOf);
        string json = JsonUtility.ToJson(data, true);

        string path = Path.Combine(Application.persistentDataPath, $"Tower_{gameObject.GetInstanceID()}.json");
        File.WriteAllText(path, json);

        Debug.Log($"Tower saved: {path}");
    }

    private void Start()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllSave += SaveTower;
        else
            Debug.LogError("SaveManager not ready when tower enabled!");
    }

    private void OnEnable()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllSave += SaveTower;
        else
            Debug.LogError("SaveManager not ready when tower enabled!");
    }

    private void OnDisable()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllSave -= SaveTower;
    }
}
