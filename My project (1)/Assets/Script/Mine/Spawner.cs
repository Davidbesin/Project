using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private BoxCollider spawnZone;   // define spawn area
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private Vector3 offset;
    public int AIToSpawn => TowerManager.Instance.TotalWaveStrength;   // number of mines based on towers
    [SerializeField] private float spacingRadius = 1f; 
    [SerializeField] private int maxAttemptsPerMine = 30;
    [SerializeField] private float spawnInterval = 180f; // 3 minutes in seconds

    private Coroutine spawnRoutine;

    // Hook this method to your UI Button
    public void StartSpawning()
    {
        if (spawnRoutine == null)
        {
            spawnRoutine = StartCoroutine(SpawnLoop());
            Debug.Log("Spawn loop started via button.");
        }
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnMines();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SpawnMines()
    {
        Debug.Log("=== SpawnMines Started ===");

        int spawnedCount = 0;

        while (spawnedCount < AIToSpawn)
        {
            bool success = false;

            for (int attempt = 0; attempt < maxAttemptsPerMine; attempt++)
            {
                if (TrySpawn())
                {
                    spawnedCount++;
                    success = true;
                    break;
                }
            }

            if (!success)
            {
                Debug.LogWarning("FAILED: Could not place mine after attempts");
                break;
            }
        }

        Debug.Log("Spawn complete. Total mines: " + spawnedCount);
    }

    private bool TrySpawn()
    {
        Vector3 randomPoint = new Vector3(
            Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x),
            spawnZone.bounds.max.y,
            Random.Range(spawnZone.bounds.min.z, spawnZone.bounds.max.z)
        );

        Vector3 rayDir = Vector3.down;
        float rayLength = spawnZone.bounds.size.y + 100f;

        if (Physics.Raycast(randomPoint, rayDir, out RaycastHit hit, rayLength))
        {
            Vector3 spawnPos = hit.point;

            // Overlap check
            Collider[] hits = Physics.OverlapSphere(spawnPos, spacingRadius);
            foreach (var col in hits)
            {
                // Ignore terrain
                if (col.TryGetComponent<Terrain>(out _)) continue;

                // Ignore spawner itself
                if (col.TryGetComponent<Spawner>(out _)) continue;

                // Block if another mine is too close
                if (col.TryGetComponent<Mine>(out _)) return false;

                // Block if collider is tagged SafeSpace
                if (col.CompareTag("SafeSpace"))
                {
                    Debug.Log("Blocked by SafeSpace zone: " + col.name);
                    return false;
                }
            }

            Quaternion rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Instantiate(minePrefab, spawnPos + offset, rot);

            return true;
        }

        return false;
    }

}
