using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AreaOfEffectDefensiveTower : BaseDefensiveTower
{
    Coroutine attack;

    [SerializeField] float timeRate;
    [SerializeField] int type;
    [SerializeField] int damage;

    int Damage => type * damage;

    public event Action onDealWithEnemies;
    public event Action GetReady;

    [SerializeField] ParticleSystem attackEffect;

    private void Awake()
    {
        attackEffect?.Stop();
        trigger = GetComponent<SphereCollider>();
        towerLevelComponent = GetComponent<TowerLevel>();

        if (!trigger || !trigger.isTrigger)
        {
            Debug.LogError("Tower requires a SphereCollider set as trigger.");
            return;
        }
    }

    private void Start()
    {
        attackEffect?.Stop();
        ApplyLevelScaling();  // same as AttackDefensiveTower
    }

    public override void OnEnable()
    {
        attackEffect?.Stop();
        if (!TowerManager.Instance.MyTowers.Contains(this))
        {
            TowerManager.Instance.JoinList(this);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        BaseEnemyAI enemy = other.GetComponent<BaseEnemyAI>();
        if (enemy == null || !enemy.gameObject.activeInHierarchy) return;

        if (!enemyWithinRange.Contains(enemy))
            enemyWithinRange.Add(enemy);

        if (attack == null)
            attack = StartCoroutine(AttackRunning(timeRate));
    }

    protected override void OnTriggerExit(Collider other)
    {
        BaseEnemyAI enemy = other.GetComponent<BaseEnemyAI>();
        if (enemy == null) return;

        enemyWithinRange.Remove(enemy);
        StopAttackIfNeeded();
    }

    IEnumerator AttackRunning(float seconds)
    {
        while (true)
        {
            CleanupEnemies();

            if (enemyWithinRange.Count == 0)
            {
                attack = null;
                attackEffect?.Stop();
                yield break;
            }

            DealWithEnemies();
            yield return new WaitForSeconds(seconds);
        }
    }

    void CleanupEnemies()
    {
        for (int i = enemyWithinRange.Count - 1; i >= 0; i--)
        {
            BaseEnemyAI enemy = enemyWithinRange[i];
            if (enemy == null || !enemy.gameObject.activeInHierarchy)
                enemyWithinRange.RemoveAt(i);
        }
    }

    void StopAttackIfNeeded()
    {
        CleanupEnemies();

        if (enemyWithinRange.Count == 0 && attack != null)
        {
            StopCoroutine(attack);
            attack = null;
            attackEffect?.Stop();
        }
    }

    protected override void DealWithEnemies()
    {
        CleanupEnemies();
        if (enemyWithinRange.Count == 0) return;

        GetReady?.Invoke();
        onDealWithEnemies?.Invoke();

        attackEffect?.Play();

        foreach (BaseEnemyAI enemy in enemyWithinRange)
        {
            if (enemy == null || !enemy.gameObject.activeInHierarchy) continue;
            enemy.TakeDamage(Damage);
        }
    }
}
