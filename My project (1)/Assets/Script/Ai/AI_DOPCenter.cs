using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_DOPCenter : MonoBehaviour
{
    public List<AiTarget> allAi = new();

    // Property that returns only active AiTargets
    public List<AiTarget> ActiveAiTargets
    {
        get
        {
            List<AiTarget> activeList = new List<AiTarget>();
            foreach (var ai in allAi)
            {
                if (ai != null && ai.gameObject.activeInHierarchy)
                {
                    activeList.Add(ai);
                }
            }
            return activeList;
        }
    }

    private void Start()
    {
        // Populate list with both active and inactive AiTargets
        allAi = new List<AiTarget>(FindObjectsOfType<AiTarget>(true));
        StartCoroutine(RepeatEveryThirdSecond());
    }

    private IEnumerator RepeatEveryThirdSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.33f);
           // Debug.Log("Coroutine tick at " + Time.time);

            // Loop only through active ones
            foreach (var ai in ActiveAiTargets)
            {
                ai.ExecuteAction();
            }
        }
    }
}
