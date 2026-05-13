using System.Collections.Generic;
using System.Collections;  
using UnityEngine;
using System;


public class BaseEnemyAI : MonoBehaviour, IEnemy, ICombatant
{
    public float moveSpeed = 4f;
    [SerializeField] private int health;
    [SerializeField] private int baseHealth;

    public bool isAttacking {get;}
    private int MaxHealth => baseHealth * level;
    [SerializeField] private int level;
    public int Health {get {return health;} set {health =  Math.Clamp(value, 0, MaxHealth);} } 
    private Rigidbody ienemyRigidBody;

    [SerializeField]int damage;
    [SerializeField] int baseDamage;

    IHealth designatedEnemy;

    public bool PlayerSide => false;

    private UpgradeableStatInterface levelUpgradeable;

    public UpgradeableStatInterface LevelUpgradeable => levelUpgradeable;
    Coroutine attack;
    [SerializeField]float timeRate = .33f;
    void Start()
    {
       
        Health = MaxHealth;
    }

    private void OnEnable()
    {
       
    }

    private void OnDisable()
    {
        
    }
    // DEFAULT IENEMY
    public void MyDefault()
    {
        moveSpeed = 10;
    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        IHealth theoriticalEnemy;
        theoriticalEnemy = other.GetComponent<IHealth>();
        if (theoriticalEnemy == null) return;
        designatedEnemy = theoriticalEnemy;
        Debug.Log("Yea Weve seen it");
        if (attack == null) attack= StartCoroutine(AttackEnemy(timeRate));
    }

    protected virtual void OnTriggerExit(Collider other) 
    {
        MyDefault();
    }

    public void TakeDamage(int damage) => Health -= damage;
    

    public void Die()
    {
        //Destroy(gameObject);
    }

    IEnumerator AttackEnemy(float seconds)
    {
        while (designatedEnemy != null)
        {
            designatedEnemy.TakeDamage(GiveDamage(PlayerSide == designatedEnemy.PlayerSide));
            yield return new WaitForSeconds(seconds);
        }
    }
    public int GiveDamage(bool side)
    {
        if(side)
            return 0;    

        else
            return damage;       
    }
    public void ApplyStatsToDamage() => damage = baseDamage * levelUpgradeable.level;


} 