using UnityEngine;


public class Mine : MonoBehaviour, IHealth
{
    
    [SerializeField] private BaseResource resourceType;

    public BaseResource ResourceType => resourceType;

    private int health;
    public int Health 
    {
        get {return health;} 
        set
        {
           health = Mathf.Clamp(value, 0, maxHealth);
        }
    }
    public bool PlayerSide => true;
    public int mineAmount;
    
    private int mineMaxAmount;
    private int regenRate;
    private int regenRateHealth;
    private int maxHealth;
    [SerializeField] int mineMaxAmountBase;
    [SerializeField] int regenRateBase;
    [SerializeField] int regenRateHealthBase;
    [SerializeField] int maxHealthBase;
    
    private void Awake()
    {
        mineMaxAmount = mineMaxAmountBase;
        regenRate = regenRateBase;
        regenRateHealth = regenRateHealthBase;
        maxHealth = maxHealthBase;
    }


    [SerializeField] Animator billboard;

    private void Update()
    {
        Regenerate();
    }

    private void Regenerate()
    {
        
        
        if (mineAmount < mineMaxAmount)
        {
           
            mineAmount += regenRate;
            if (mineAmount > mineMaxAmount)
                mineAmount = mineMaxAmount;
        }

        if (Health < maxHealth)
        {
            Health += regenRateHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            // destruction logic
        }
    }

    public int Collect(int requestedAmount)
    {
        int collected = Mathf.Min(requestedAmount, mineAmount);
        mineAmount -= collected;
        return collected;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (billboard == null) return;
        billboard.SetBool("Appear", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (billboard == null) return;
        billboard.SetBool("Appear", false);
    }

    public void UpgradeMineCapacity(int level)
    {
        mineMaxAmount = mineMaxAmountBase * level;
    }

    public void UpgradeRegenRate(int level)
    {
        regenRate = regenRateBase * level;
    }

    public void UpgradeHealth(int level)
    {
        maxHealth = maxHealthBase * level;
    }

    public void UpgradeRegenHealth(int level)
    {
        regenRateHealth = regenRateHealthBase * level;
    }

    public void ResetToBaseStats()
    {
        mineMaxAmount = mineMaxAmountBase;
        regenRate = regenRateBase;
        regenRateHealth = regenRateHealthBase;
        maxHealth = maxHealthBase;
        Health = maxHealth;
    }

    public void AssignResource(BaseResource resource)
    {
        resourceType = resource;
    }
}
