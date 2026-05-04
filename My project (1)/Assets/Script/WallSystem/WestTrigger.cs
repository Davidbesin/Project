    using UnityEngine;
    using System.Collections;

    public class WestTrigger : Trigger
    {
        [SerializeField] GameObject westWall;
        EastTrigger triggerSmoochingPartner;

        void OnTriggerEnter(Collider other)
        {
            triggerSmoochingPartner = other.GetComponent<EastTrigger>();
            if (triggerSmoochingPartner == null) return;
             if (!triggerSmoochingPartner.own) return;

            StartCoroutine(CheckLogic());
        }

        IEnumerator CheckLogic()
        {
            while (true)
            {
                if (own)
                    westWall.SetActive(false);
                else
                    westWall.SetActive(true);

                yield return new WaitForSeconds(0.33f);
            }
        }
    }
