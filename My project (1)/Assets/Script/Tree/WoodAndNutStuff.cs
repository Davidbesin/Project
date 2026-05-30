using UnityEngine;

public class WoodAndNutStuff : MonoBehaviour
{
    TreeAndStumps[] trees;

    void Start()
    {
        trees = FindObjectsOfType<TreeAndStumps>();
    }

    void Update()
    {
        for (int i = 0; i < trees.Length; i++)
        {
            CapsuleCollider collider = trees[i].GetComponent<CapsuleCollider>();

            if (trees[i].CurrentStage == TreeAndStumps.Stage.Stump2)
            {
                collider.enabled = false;
            }
            else
            {
                collider.enabled = true;
            }
        }
    }
}