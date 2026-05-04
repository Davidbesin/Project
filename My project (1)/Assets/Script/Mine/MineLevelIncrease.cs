using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System;


public class MineLevelIncrease : MonoBehaviour, ITransact
{
    [Tooltip("Player inventory reference.")]
    public PlayersInventory playerBag;

    [Tooltip("Player second bigger inventory reference.")]
    public PlayersInventory playerVault;
    private MineLevel mineLevel;
    [Header("Unlocks & Transactions")]
    private int currentIndex => mineLevel.Level - 1;
    public List<TransactionSet> toSpend = new();
    public bool HasBought { get; private set; }
    UnityEvent TransactionEvent;
    
    public bool AutoTransact => false;

    public Action toAllowBuy => AllowBuyToggleOn;
    public Action toNotAllowBuy => AllowBuyToggleOff;

    bool allowBuy;
    public bool AllowBuy 
    {
        get
        {
            return allowBuy;
        }   
        private set
        {
            if (value) TransactionEvent?.Invoke();
        }
    }
   

    void Awake()
    {
        mineLevel = GetComponent<MineLevel>();
    }

    void OnEnable()
    {
        //TransactionEvent.Addlistener();
    }

    public bool DoYourTransaction()
    {
        if (!AllowBuy) return false;
        if (toSpend == null || toSpend.Count <= currentIndex) return false;

        List<TransactionItem> transactList = toSpend[currentIndex].items;

        // Check affordability
        foreach (var transact in transactList)
        {
            bool canPay = playerBag.CanSubtract(transact.resource.GetType(), transact.amount) ||
                          playerVault.CanSubtract(transact.resource.GetType(), transact.amount);

            if (!canPay) return false;
        }

        // Commit deductions
        foreach (var transact in transactList)
        {
            if (!playerBag.SubtractResources(transact.resource.GetType(), transact.amount))
            {
                if (!playerVault.SubtractResources(transact.resource.GetType(), transact.amount)) return false;
            }
        }

        Debug.Log("Upgrade transaction complete");
        HasBought = true;

        // Trigger level increase
        mineLevel.IncreaseLevel();
        return HasBought;
    }

    public void AllowBuyToggleOn()
    {
        AllowBuy = true;
    }

    public void AllowBuyToggleOff()
    {
        AllowBuy = false;
    }
}
