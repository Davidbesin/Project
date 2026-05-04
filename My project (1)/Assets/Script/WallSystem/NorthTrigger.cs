using UnityEngine;
using System.Collections;

public class NorthTrigger : Trigger
{
    [SerializeField] GameObject northWall;

    SouthTrigger triggerSmoochingPartner; 

    void OnTriggerEnter(Collider other)
    {
        triggerSmoochingPartner = other.GetComponent<SouthTrigger>();
        if (triggerSmoochingPartner == null) return;
        if (!triggerSmoochingPartner.own) return;

        StartCoroutine(CheckLogic());
    }

    IEnumerator CheckLogic()
    {
        
        while (true)
        {
            if(own)
            northWall.SetActive(false);
            else
            {
                 northWall.SetActive(true);
            }
            yield return new WaitForSeconds(0.33f); // keep waiting until condition is true
        }        
    }
}
