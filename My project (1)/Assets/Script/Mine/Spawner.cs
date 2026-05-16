using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [Header("Pool Parents")]
    [SerializeField] private Transform northParent;
    [SerializeField] private Transform eastParent;
    [SerializeField] private Transform westParent;

    [Header("Location")]
    [SerializeField] private Transform north;
    [SerializeField] private Transform east;
    [SerializeField] private Transform west;

    [Header("Pools")]
    [SerializeField] private List<GameObject> northAI = new List<GameObject>();
    [SerializeField] private List<GameObject> eastAI  = new List<GameObject>();
    [SerializeField] private List<GameObject> westAI  = new List<GameObject>();

    private List<List<GameObject>> allPools = new List<List<GameObject>>();

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 180f;

    [SerializeField] private int totalWaveStrength;

    public int AIToSpawn => TowerManager.Instance.TotalWaveStrength;

    private Coroutine spawnRoutine;
    private int directionIndex;

    private void Awake()
    {
        // Fill each pool from its parent transform
        FillPool(northParent, northAI);
        FillPool(eastParent, eastAI);
        FillPool(westParent, westAI);

        // Collect all pools into one list-of-lists
        allPools.Clear();
        allPools.Add(northAI);
        allPools.Add(eastAI);
        allPools.Add(westAI);
    }

    private void Update()
    {
        // keep inspector value in sync
        totalWaveStrength = TowerManager.Instance.TotalWaveStrength;
    }

    private void FillPool(Transform parent, List<GameObject> pool)
    {
        pool.Clear();
        if (parent == null) return;

        foreach (Transform child in parent)
        {
            pool.Add(child.gameObject);
        }
    }

    public void StartSpawning()
    {
        if (spawnRoutine == null)
        {
            spawnRoutine = StartCoroutine(SpawnLoop());
            Debug.Log("Spawn loop started.");
        }
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnWave();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnWave()
    {
        int spawnedCount = 0;

        while (spawnedCount < AIToSpawn)
        {
            if (ActivateNextDirection())
            {
                spawnedCount++;
            }
            else
            {
                Debug.LogWarning("No inactive AI available in current pool.");
                break;
            }
        }

        Debug.Log("Spawn complete. Spawned: " + spawnedCount);
    }


    public bool ActivateNextDirection()
    {
        List<GameObject> currentPool = allPools[directionIndex];
        GameObject ai = GetInactiveAI(currentPool);

      
        bool success = false;
        AiTarget target;
        if (ai != null)
        {
            ai.SetActive(true);
            target = ai.GetComponent<AiTarget>();
            target?.GoTo();
            success = true;
            if (directionIndex == 0)
            ai.transform.position = north.position;
    
            else if (directionIndex == 1)
            ai.transform.position = east.position;

            else if (directionIndex == 2)
            ai.transform.position = west.position;

            target?.GoTo();
        }
        
       

        // increment always happens
        directionIndex++;
        if (directionIndex >= allPools.Count)
            directionIndex = 0;

        return success;
    }


    private GameObject GetInactiveAI(List<GameObject> pool)
    {
        foreach (GameObject ai in pool)
        {
            if (!ai.activeInHierarchy)
                return ai;
        }
        return null;
    }
}
