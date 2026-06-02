using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_DOPCenter : MonoBehaviour
{
    public List<AiTarget> allAi = new();

    [SerializeField] private float stopDistance = 2f;
    private float stopDistanceSqr;

    public List<AiTarget> ActiveAiTargets
    {
        get
        {
            List<AiTarget> activeList = new();

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
        stopDistanceSqr = stopDistance * stopDistance;

        allAi = new List<AiTarget>(FindObjectsOfType<AiTarget>(true));

        StartCoroutine(RepeatEveryThirdSecond());
    }

    private IEnumerator RepeatEveryThirdSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.33f);

            foreach (var ai in ActiveAiTargets)
            {
                if (ai == null || ai.agent == null)
                    continue;

                ai.ExecuteAction();

                if (ai.anim != null)
                {
                    ai.anim.ExecuteAction();
                }
                
                if (!ai.agent.hasPath)
                    continue;

                float distanceSqr =
                    (ai.transform.position - ai.agent.destination).sqrMagnitude;

                if (distanceSqr < stopDistanceSqr)
                {
                    ai.agent.speed = 0f;
                }
                else
                {
                    ai.agent.speed = ai.baseSpeed;
                }

               
            }
        }
    }
}