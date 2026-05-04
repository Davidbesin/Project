using UnityEngine;
using System.Collections.Generic;

public abstract class BaseDefensiveTower : MonoBehaviour, IWaveContributor, IHealth
{
    public List<BaseEnemyAI> enemyWithinRange = new();

    [Header("BAsic Tower Stats")]
    [SerializeField] protected int towerLevel = 1;
    [SerializeField] protected int baseHealth = 100;
    [SerializeField] protected int regenRateHealth;
    [SerializeField]protected int health;
    protected float range;
    public bool PlayerSide => true;
    private SphereCollider trigger;
   // private Planet planet;

    public int Health 
    { 
        get => health; 
        set 
        {
            health = Mathf.Clamp(value, 0, MaxHealth);
        } 
    }

    public float Range => range;
    public int MaxHealth ;

    public int TowerLevel
    {
        get => towerLevel;
        set
        {
            towerLevel = Mathf.Max(1, value);
            UpdateStats();
        }
    }


    private void Awake() 
    {
        trigger = GetComponent<SphereCollider>();
        if (!trigger || !trigger.isTrigger)
        {
            Debug.LogError("Tower requires a SphereCollider set as trigger.");
            return;
        }

        UpdateStats();
    }

    private void UpdateStats()
    {
        health = MaxHealth;

    }

    private void Update()
    {
        health += regenRateHealth;
    }
    public virtual void OnEnable()
    {
        UpdateStats();
      //  planet = Player.Instance.ResidingPlanet;
       // planet.JoinList(this);
        TowerManager.Instance.JoinList(this);
    }

    public virtual void OnDisable()
    {
       // planet = Player.Instance.ResidingPlanet;
        //planet.GetOutOftheList(this);
        TowerManager.Instance.GetOutOfList(this);
    }

    public int ContributeToWave() => towerLevel;
    public void TakeDamage(int damage) => Health -= damage;

    protected virtual void OnTriggerEnter(Collider other)
    {
        BaseEnemyAI enemy = other.GetComponent<BaseEnemyAI>();
        if (enemy == null) return;

        if (!enemyWithinRange.Contains(enemy))
            enemyWithinRange.Add(enemy);

        DealWithEnemies();
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        BaseEnemyAI enemy = other.GetComponent<BaseEnemyAI>();
        if (enemy == null) return;

        enemyWithinRange.Remove(enemy);
    }

    public void SetRange(float range)
    {
        this.range = range;
        if (trigger != null)
            trigger.radius = range;
    }

    protected abstract void DealWithEnemies();



 /*   // --- Methods to affect stats (Mine-style) ---
      [SerializeField] UpgradeableStatInterface upgradeablehealth;
    public void ApplyStatsToMaxHealth(int health)
    {
        MaxHealth = baseHealth * upgradeablehealth.level;
    }

    int regenRateHealth;
    [SerializeField]int baseRegenRateHealth;
    [SerializeField] UpgradeableStatInterface upgradeableRegenhealth;
    public void ApplyStatsToRegenHealth()
    {
       regenRateHealth = baseRegenRateHealth * upgradeableRegenhealth.level;
    }

    [SerializeField]float baseRange;
    [SerializeField] UpgradeableStatInterface upgradeableRange;
    public void ApplyStatsToRange()
    {
        range = baseRange * upgradeableRange.level;
    } */

    public void ApplyStatsToTowerLevel(int level)
    {
        this.towerLevel = Mathf.Max(1, level);
        UpdateStats();
    }
}
