using UnityEngine;
using System;

public class TileTransact : MonoBehaviour, ITransact
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Tile tile;
    TransactionSet transactList = new();
    public bool AutoTransact => true;
    public bool AllowBuy => false;
    private void Awake()
    {
        tile = GetComponent<Tile>();    
    }
   
    public Action toAllowBuy => null;
    public Action toNotAllowBuy => null;
    // Update is called once per frame
    void Update()
    {
        
    }
    [Tooltip("Player inventory reference.")]
    public PlayersInventory playerBag;

    [Tooltip("Player second bigger inventory reference.")]
    public PlayersInventory playerVault;

    public bool HasBought { get; private set; }

    public bool DoYourTransaction()
    {

        foreach (var transact in transactList.items)
        {
            if (!playerBag.SubtractResources(transact.resource.GetType(), transact.amount))
            {
                 if (!playerVault.SubtractResources(transact.resource.GetType(), transact.amount)) return false;
            }
        }

        
        HasBought = true;
        tile.SetOwned(HasBought);
        return HasBought;
    }
}
