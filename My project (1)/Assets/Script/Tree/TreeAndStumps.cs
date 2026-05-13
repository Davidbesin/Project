using UnityEngine;
using System.Collections;

public class TreeAndStumps : MonoBehaviour
{
    [SerializeField] private GameObject fullTree;
    [SerializeField] private GameObject treeStump1;
    [SerializeField] private GameObject treeStump2;
    [SerializeField]PlayersInventory playersInventory;
    [SerializeField]BaseResource wood;

    [SerializeField] int woodToGet;

    public enum Stage
    {
        Full,
        Stump1,
        Stump2
    }

    [SerializeField] private Stage currentStage;

    public Stage CurrentStage
    {
        get => currentStage;
        set
        {
            currentStage = value;
            UpdateVisuals();
        }
    }

    void Start()
    {
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        // Disable all first
        fullTree.SetActive(false);
        treeStump1.SetActive(false);
        treeStump2.SetActive(false);

        // Enable only the one matching currentStage
        switch (currentStage)
        {
            case Stage.Full:
                fullTree.SetActive(true);
                StopAllCoroutines();
                break;
            case Stage.Stump1:
                treeStump1.SetActive(true);
                break;
            case Stage.Stump2:
                treeStump2.SetActive(true);
                // start regeneration coroutine
                StartCoroutine(RegenerateTree());
                break;
        }
    }

    IEnumerator RegenerateTree()
    {
        // Wait 20 seconds before regeneration starts
        yield return new WaitForSeconds(20f);

        // Move back to stump1
        CurrentStage = Stage.Stump1;
        yield return new WaitForSeconds(5f);

        // Finally back to full tree
        CurrentStage = Stage.Full;
    }

    void OnTriggerEnter(Collider other)
    {
        Axe axe = other.GetComponent<Axe>();
        if (axe == null) return;
        
        Debug.Log("CutGrass");
        // Cycle through stages when hit by axe
        if (currentStage == Stage.Full)
            CurrentStage = Stage.Stump1;
        else if (currentStage == Stage.Stump1)
            CurrentStage = Stage.Stump2;
        //Wood add
        playersInventory.AddResources(wood.GetType(), woodToGet); 


    }




    public void SetTheNut(int nut)
    {
        woodToGet = nut;
    }
}
