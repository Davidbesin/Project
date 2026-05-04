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

    [SerializeField] ParticleSystem attackEffect;  // Particle system reference

    void OnTriggerEnter(Collider other)
    {
        BaseEnemyAI enemy = other.GetComponent<BaseEnemyAI>();
        if (enemy == null) return;

        enemyWithinRange.Add(enemy);
        if (attack == null) attack = StartCoroutine(AttackRunning(timeRate));
    }

    void OnTriggerExit(Collider other)
    {
        BaseEnemyAI enemy = other.GetComponent<BaseEnemyAI>();
        if (enemy == null) return;

        enemyWithinRange.Remove(enemy);
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

    IEnumerator AttackRunning(float seconds)
    {
        while (enemyWithinRange.Count > 0)
        {
            DealWithEnemies();
            yield return new WaitForSeconds(seconds);
        }
    }

    protected override void DealWithEnemies()
    {
        GetReady?.Invoke();
        enemyWithinRange.RemoveAll(e => e == null);

        if (enemyWithinRange.Count == 0) return;

        BaseEnemyAI target = enemyWithinRange[0];
        if (target == null) return;

        designatedTarget = target;
        onDealWithEnemies?.Invoke();

        if (attackEffect != null)
        {
            attackEffect.Play();
        }

        target.TakeDamage(Damage);
    }

}
