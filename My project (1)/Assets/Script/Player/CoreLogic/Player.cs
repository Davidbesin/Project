using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerToMine), typeof(PlayerToDefensiveTower),
typeof(PlayerToSpend))]
public class Player : MonoBehaviour, ICombatant
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public static Player Instance {get; private set;}
    
    public bool PlayerSide => true;
    [SerializeField]private int health;
    public float playerMoveSpeed; 
    private int MaxHealth;
    int damage;
    public int Health
    {
        get => health;
        private set => health = Mathf.Clamp(value, 0, MaxHealth);
    }
    
    
    public int GatherSpeed {get; private set;} = 100;

    private PlayerToMine myMineOperations;
    
    private PlayerToDefensiveTower myDefensiveTowerOperations;
    public List<BaseResource> playersInventory {get; set;} = new();

    
    private void Awake() 
    {
        Instance = this;
    }
 
   
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("Player died");
        }
    }


    [SerializeField] bool stillSummoning;
     public int GiveDamage(bool side)
    {
        if(side)
        {
            return 0;
        }

        else
        {
            return damage;
        }
        
    }
}
