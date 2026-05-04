using UnityEngine;

public class MineLevel : MonoBehaviour, ILevel
{
    [SerializeField] private int level = 1;
    private Mine personalMine;

    public int Level => level;
    public string LevelText => Level.ToString();

    void Awake()
    {
        personalMine = GetComponent<Mine>();
    }

    public void ApplyAllUpgrades()
    {
        personalMine.UpgradeHealth(level);
        personalMine.UpgradeRegenRate(level);
        personalMine.UpgradeMineCapacity(level);
        personalMine.UpgradeRegenHealth(level);
    }

    public void IncreaseLevel()
    {
        level++;
        ApplyAllUpgrades();
    }

     public void ResetLevel()
    {
        level = 0;
    }
}
