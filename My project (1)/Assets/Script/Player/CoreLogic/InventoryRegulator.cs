using UnityEngine;
using System.Collections;

public class InventoryRegulator : MonoBehaviour
{
    [SerializeField] private PlayersInventory playerBag;
    [SerializeField] private int maxAllowedSum = 100;   // threshold
    [SerializeField]int sum;
    private void Start()
    {
        // Start the coroutine when the game begins
        StartCoroutine(RegulateInventory());
    }

    private IEnumerator RegulateInventory()
    {
        // Loop forever
        while (true)
        {
            sum = 0;
            foreach (var resource in playerBag.PlayersResources)
            {
                sum += resource.Amount;
            }

            // Gate logic: flip boolean when sum exceeds threshold
            playerBag.canAddResources = sum < maxAllowedSum;
         //    if(!playerBag.canAddResources) Debug.Log("BagPack Full");
            // Wait ~0.33 seconds (3 times per second)
            yield return new WaitForSeconds(0.33f);
        }
    }
}
