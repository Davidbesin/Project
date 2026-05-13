using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerToMine : MonoBehaviour
{
    public int resourceGatheringTime = 1; // seconds per tick
    public int whatToCollect;
    [SerializeField]PlayersInventory playersInventory;

    private Coroutine collectingCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        Mine currentMine = other.GetComponent<Mine>();
        if (currentMine != null)
        {
            if (collectingCoroutine != null)
                StopCoroutine(collectingCoroutine);

            collectingCoroutine = StartCoroutine(CollectFromMine(currentMine));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Mine currentMine = other.GetComponent<Mine>();
        if (currentMine != null)
        {
            if (collectingCoroutine != null)
                StopCoroutine(collectingCoroutine);
        }
    }

    private IEnumerator CollectFromMine(Mine mine)
    {
        while (true)
        {
            yield return new WaitForSeconds(resourceGatheringTime);
            int collectedAmount = mine.Collect(whatToCollect); 

            if (collectedAmount > 0)
            {
                Debug.Log($"Collected {collectedAmount} from {mine.ResourceType.GetType().Name}. Mine now has {mine.mineAmount} left.");
                playersInventory.AddResources(mine.ResourceType.GetType(), collectedAmount); 
            }
        }
    }
}