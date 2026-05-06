using UnityEngine;
using System.Collections.Generic;

public abstract class BaseDefensiveTower : MonoBehaviour, IWaveContributor, IHealth
{
    public List<BaseEnemyAI> enemyWithinRange = new();

    [Header("Basic Tower Stats")]
    [SerializeField] protected int towerLevel = 1;
    [SerializeField] protected int baseHealth = 100;
    [SerializeField] protected int regenRateHealth;
    [SerializeField] protected int health;
    protected float range;
    public bool PlayerSide => true;
    private SphereCollider trigger;

    public int Health 
    { 
        get => health; 
        set => health = Mathf.Clamp(value, 0, MaxHealth); 
    }

    public float Range => range;
    public int MaxHealth;

    public int TowerLevel
    {
        get => towerLevel;
        set
        {
            towerLevel = Mathf.Max(1, value);
            UpdateStats();
            TowerManager.Instance.RecalculateWaveStrength(); // update manager when level changes
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
        MaxHealth = baseHealth; // you can expand this with upgrades later
        health = MaxHealth;
    }

    private void Update()
    {
        health = Mathf.Min(MaxHealth, health + regenRateHealth);
    }

    public virtual void OnEnable()
    {
        UpdateStats();
        TowerManager.Instance.JoinList(this); // register with manager
    }

    public virtual void OnDisable()
    {
        TowerManager.Instance.GetOutOfList(this); // unregister from manager
    }

    // IWaveContributor implementation
    public int ContributeToWave() => towerLevel;

    // IHealth implementation
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

    public void ApplyStatsToTowerLevel(int level)
    {
        this.towerLevel = Mathf.Max(1, level);
        UpdateStats();
        TowerManager.Instance.RecalculateWaveStrength(); // keep manager updated
    }
}
