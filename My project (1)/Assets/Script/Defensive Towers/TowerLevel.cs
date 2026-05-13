using UnityEngine;

public class TowerLevel : MonoBehaviour, ILevel
{
    [SerializeField] UpgradeableStatInterface statLevel;

    public int Level => Mathf.Max(1, statLevel.level);
    public string LevelText => Level.ToString();

    private BaseDefensiveTower tower;

    private void Awake()
    {
        tower = GetComponent<BaseDefensiveTower>();
        statLevel = GetComponent<UpgradeableStatInterface>();
    }

    public void ApplyAllUpgrades()
    {
        tower.ApplyLevelScaling();
        TowerManager.Instance.RecalculateWaveStrength();
    }

    public void IncreaseLevel()
    {
        statLevel.level++;
        ApplyAllUpgrades();
    }

    public void ResetLevel()
    {
        statLevel.level = 1;
        ApplyAllUpgrades();
    }
}
