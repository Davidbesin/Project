using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class AI_DOPCenter : MonoBehaviour
{
    public List<AiTarget> allAi = new();

 [ContextMenu("Spawn Mines")]
    private void Start()
    {
        // Start the coroutine when the object is created
        StartCoroutine(RepeatEveryThirdSecond());
    }

    private IEnumerator RepeatEveryThirdSecond()
    {
        while (true)
        {
            // Your repeated logic goes here
            yield return new WaitForSeconds(0.33f);
            Debug.Log("Coroutine tick at " + Time.time);

            // Example: iterate through all AI targets
            foreach (var ai in allAi)
            {
                ai.ExecuteAction();
            }

            // Wait 0.33 seconds before repeating
            
        }
    }
}
