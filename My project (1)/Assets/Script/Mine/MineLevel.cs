using UnityEngine;

public class MineLevel : MonoBehaviour, ILevel
{
    
    [SerializeField] UpgradeableStatInterface statlevel;
    public int level => statlevel.level;
    private Mine personalMine;

    public int Level => level;
    public string LevelText => Level.ToString();

    void Awake()
    {
        personalMine = GetComponent<Mine>();
        statlevel = GetComponent<UpgradeableStatInterface>();
    }


    public void ApplyAllUpgrades()
    {
       // personalMine.UpgradeHealth(level);
        personalMine.UpgradeRegenRate(level);
        personalMine.UpgradeMineCapacity(statlevel.level);
       // personalMine.UpgradeRegenHealth(statlevel.level);
    }

    public void IncreaseLevel()
    {
        statlevel.level++;
        ApplyAllUpgrades();
    }

     public void ResetLevel()
    {
        statlevel.level = 0;
    }
}
