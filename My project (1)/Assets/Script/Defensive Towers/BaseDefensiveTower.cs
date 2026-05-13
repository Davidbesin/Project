using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(TowerLevel))]
[RequireComponent(typeof(SphereCollider))]
public abstract class BaseDefensiveTower : MonoBehaviour, IWaveContributor, IHealth
{
    public List<BaseEnemyAI> enemyWithinRange = new();

    [Header("Base Tower Stats")]
    [SerializeField] protected int baseHealth = 100;
    [SerializeField] protected int baseRegenRateHealth = 1;
    [SerializeField] protected float baseRange = 5f;

    [Header("Current Tower Stats")]
    [SerializeField] protected int health;
    [SerializeField] protected int regenRateHealth;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected float range;

    public bool PlayerSide => true;
    protected SphereCollider trigger;
    protected TowerLevel towerLevelComponent;

    public int Health 
    { 
        get => health; 
        set => health = Mathf.Clamp(value, 0, maxHealth); 
    }

    public float Range => range;
    public int MaxHealth => maxHealth;

    private void Awake() 
    {
        trigger = GetComponent<SphereCollider>();
        towerLevelComponent = GetComponent<TowerLevel>();

        if (!trigger || !trigger.isTrigger)
        {
            Debug.LogError("Tower requires a SphereCollider set as trigger.");
            return;
        }
    }

     
    private void Update()
    {
        health = Mathf.Min(maxHealth, health + regenRateHealth);
    }

    public virtual void OnEnable()
    {
       //s ApplyLevelScaling();
        if  (!TowerManager.Instance.MyTowers.Contains(this))
           { if (TowerManager.Instance == null) Debug.Log("bull dg");
           TowerManager.Instance.JoinList(this);}

            Debug.Log("dhdg");
    }

    public virtual void OnDisable()
    {
        TowerManager.Instance.GetOutOfList(this);
    }

    // IWaveContributor implementation
    public int ContributeToWave() => towerLevelComponent.Level;

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

    // 🔑 Scaling method
    public void ApplyLevelScaling()
    {
        int level = towerLevelComponent.Level;

        maxHealth = baseHealth * level;
        regenRateHealth = baseRegenRateHealth * level;
        range = baseRange * level;

        health = maxHealth;

        if (trigger != null)
            trigger.radius = range; // range directly drives collider radius
    }

    protected abstract void DealWithEnemies();
}
