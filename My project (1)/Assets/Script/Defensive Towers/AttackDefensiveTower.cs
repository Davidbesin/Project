using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AttackDefensiveTower : BaseDefensiveTower
{
    Coroutine attack;

    [SerializeField] float timeRate;
    [SerializeField] int type;
    [SerializeField] int damage;

    int Damage => type * damage;

    public event Action onDealWithEnemies;
    public event Action GetReady;

    public BaseEnemyAI designatedTarget;

    [SerializeField] ParticleSystem attackEffect;

    void Start()
    {
        if (attackEffect != null)
        {
            attackEffect.Stop();
        }
    }

   protected override void OnTriggerEnter(Collider other)
    {
        BaseEnemyAI enemy = other.GetComponent<BaseEnemyAI>();

        if (enemy == null) return;

        if (!enemy.gameObject.activeInHierarchy) return;

        if (!enemyWithinRange.Contains(enemy))
        {
            enemyWithinRange.Add(enemy);
        }

        if (attack == null)
        {
            attack = StartCoroutine(AttackRunning(timeRate));
        }
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

                if (attackEffect != null)
                {
                    attackEffect.Stop();
                }

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
            {
                enemyWithinRange.RemoveAt(i);
            }
        }
    }

    void StopAttackIfNeeded()
    {
        CleanupEnemies();

        if (enemyWithinRange.Count == 0 && attack != null)
        {
            StopCoroutine(attack);

            attack = null;

            if (attackEffect != null)
            {
                attackEffect.Stop();
            }
        }
    }

    protected override void DealWithEnemies()
    {
        CleanupEnemies();

        if (enemyWithinRange.Count == 0)
            return;

        GetReady?.Invoke();

        BaseEnemyAI target = enemyWithinRange[0];

        if (target == null) return;

        if (!target.gameObject.activeInHierarchy) return;

        designatedTarget = target;

        onDealWithEnemies?.Invoke();

        if (attackEffect != null)
        {
            attackEffect.Play();
        }

        target.TakeDamage(Damage);
    }
}