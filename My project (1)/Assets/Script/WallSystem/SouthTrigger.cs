using UnityEngine;
using System.Collections;

public class SouthTrigger : Trigger
{
    [SerializeField] GameObject southWall;
    NorthTrigger triggerSmoochingPartner;

    void OnTriggerEnter(Collider other)
    {
        triggerSmoochingPartner = other.GetComponent<NorthTrigger>();
        if (triggerSmoochingPartner == null) return;
         if (!triggerSmoochingPartner.own) return;


        StartCoroutine(CheckLogic());
    }

    IEnumerator CheckLogic()
    {
        while (true)
        {
            if (own)
                southWall.SetActive(false);
            else
                southWall.SetActive(true);

            yield return new WaitForSeconds(0.33f);
        }
    }
}
