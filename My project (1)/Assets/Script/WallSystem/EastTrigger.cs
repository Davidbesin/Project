using UnityEngine;
using System.Collections;

public class EastTrigger : Trigger
{
    [SerializeField] GameObject eastWall;
    WestTrigger triggerSmoochingPartner;

    void OnTriggerEnter(Collider other)
    {
        triggerSmoochingPartner = other.GetComponent<WestTrigger>();
        if (triggerSmoochingPartner == null) return;
         if (!triggerSmoochingPartner.own) return;


        StartCoroutine(CheckLogic());
    }

    IEnumerator CheckLogic()
    {
        while (true)
        {
            if (own)
                eastWall.SetActive(false);
            else
                eastWall.SetActive(true);

            yield return new WaitForSeconds(0.33f);
        }
    }
}
