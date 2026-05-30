using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    // =====================================================
    // DATA STRUCTURES
    // =====================================================
    [System.Serializable]
    public class SpawnLane
    {
        public Transform spawnPoint;
        public List<Transform> positions = new();

        [HideInInspector] public List<Transform> unused = new();
        [HideInInspector] public List<Transform> used = new();
    }

    // =====================================================
    // LANES
    // =====================================================
    [Header("Lanes")]
    [SerializeField] private SpawnLane north;
    [SerializeField] private SpawnLane east;
    [SerializeField] private SpawnLane west;

    private List<SpawnLane> lanes = new();
    int laneIndex;

    // =====================================================
    // ENEMY POOL
    // =====================================================
    private static List<BaseEnemyAI> enemyCache = new();

    [Header("Debug")]
    [SerializeField] private int enemyCount;

    // =====================================================
    // WAVE SYSTEM
    // =====================================================
    private Coroutine waveRoutine;

    // =====================================================
    // LIFECYCLE
    // =====================================================
    private void Awake()
    {
        lanes.Add(north);
        lanes.Add(east);
        lanes.Add(west);

        InitializeLane(north);
        InitializeLane(east);
        InitializeLane(west);
    }

    private void Start()
    {
        enemyCache.AddRange(FindObjectsOfType<BaseEnemyAI>(true));

        waveRoutine = StartCoroutine(WaveLoop());
    }

    private void Update()
    {
        enemyCount = enemyCache.Count;
    }

    // =====================================================
    // LANE INITIALIZATION
    // =====================================================
    void InitializeLane(SpawnLane lane)
    {
        lane.unused.Clear();
        lane.used.Clear();
        lane.unused.AddRange(lane.positions);
    }

    // =====================================================
    // YOUR ORIGINAL SPAWN (UNCHANGED)
    // =====================================================
    void SpawnEnemy()
    {
        SpawnLane lane = ChooseLane();
        BaseEnemyAI enemy = GetInactiveEnemy(out bool poolHasSpace);

        if (!poolHasSpace || enemy == null)
            return;

        enemy.transform.position = lane.spawnPoint.position;
        enemy.gameObject.SetActive(true);

        AiTarget aiTarget = enemy.gameObject.GetComponent<AiTarget>();
        aiTarget.targetPoint = GetRandomDestination(lane);
    }

    // =====================================================
    // WAVE LOOP (3 MINUTES)
    // =====================================================
    IEnumerator WaveLoop()
    {
        while (true)
        {
            yield return StartCoroutine(SpawnWave());
            yield return new WaitForSeconds(180f); // 3 minutes
        }
    }

    // =====================================================
    // WAVE SPAWN (LOOPS YOUR FUNCTION)
    // =====================================================
    IEnumerator SpawnWave()
    {
        int totalToSpawn = TowerManager.Instance.TotalWaveStrength;

        int spawned = 0;
        int safety = 0;

        while (spawned < totalToSpawn && safety < totalToSpawn * 2)
        {
            SpawnEnemy();
            spawned++;

            safety++;
            yield return null; // spreads spawn over frames (safe)
        }
    }

    // =====================================================
    // LANE LOGIC
    // =====================================================
    SpawnLane ChooseLane()
    {
        SpawnLane currentLane = lanes[laneIndex];

        laneIndex++;

        if (laneIndex >= lanes.Count)
            laneIndex = 0;

        return currentLane;
    }

    Transform GetRandomDestination(SpawnLane lane)
    {
        if (lane.unused.Count == 0)
        {
            lane.unused.AddRange(lane.used);
            lane.used.Clear();
        }

        int index = Random.Range(0, lane.unused.Count);

        Transform t = lane.unused[index];

        lane.used.Add(t);
        lane.unused.RemoveAt(index);

        return t;
    }

    // =====================================================
    // POOL ACCESS ONLY
    // =====================================================
    public static BaseEnemyAI GetInactiveEnemy(out bool poolHasSpace)
    {
        poolHasSpace = false;

        for (int i = 0; i < enemyCache.Count; i++)
        {
            if (!enemyCache[i].gameObject.activeInHierarchy)
            {
                poolHasSpace = true;
                return enemyCache[i];
            }
        }

        return null;
    }
}