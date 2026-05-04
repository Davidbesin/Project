using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    [SerializeField] private Transform planet;
    [SerializeField] private Transform BoxZoneXPos;
    [SerializeField]private Transform BoxZoneXNeg;
    [SerializeField]private Transform BoxZoneZPos;   
    [SerializeField]private Transform BoxZoneZNeg;
    [SerializeField] Vector3 offset;
    [Header("Mine")]
    [SerializeField] private GameObject minePrefab;

    [Header("Layers")]
    [SerializeField] private LayerMask TerrainLayer;

    [Header("Validation")]
    [SerializeField] private float spacingRadius = 2f;
    
    [SerializeField]int maxAttempts = 30;


    [ContextMenu("Spawn Mines")]
    public void SpawnMine()
    {
        Debug.Log("=== SpawnMine Started ===");

        for (int i = 0; i <= maxAttempts; i++)
        {
            Debug.Log("Attempt: " + (i + 1));

            if (TrySpawn())
            {
                Debug.Log("SUCCESS: Mine spawned");
            }
        }

        Debug.LogWarning("FAILED: No valid tile found");
    }

    [ContextMenu("Spawn Mine")]
    private bool TrySpawn()
    {
        
        Vector3 startPoint = new Vector3(Random.Range(BoxZoneXNeg.position.x, BoxZoneXPos.position.x), BoxZoneXPos.position.y, Random.Range(BoxZoneZNeg.position.z, BoxZoneZPos.position.z));
        float worldRadius = 1000f;
        Vector3 rayDir = -Vector3.up;

        Debug.DrawRay(
            startPoint,
            rayDir * worldRadius * 2f,
            Color.red,
            5f
        );

        if (Physics.Raycast(
            startPoint,
            rayDir,
            out RaycastHit hit,
            worldRadius * 2f,
            TerrainLayer))
        {
            Debug.Log("Ray HIT: " + hit.collider.name);

            Vector3 spawnPos = hit.point;

            // -------------------------
            // OVERLAP VALIDATION
            // -------------------------
            bool blocked = false;

            Collider[] hits = Physics.OverlapSphere(spawnPos, spacingRadius);

            foreach (var col in hits)
            {
                Debug.Log("Overlap found: " + col.name);

                if (col.GetComponent<Terrain>() != null)
                {
                    Debug.Log("Ignored HexTile: " + col.name);
                    continue;
                }

               /*  if (col.GetComponent<Water>() != null)
                {
                    Debug.Log("Ignored Water: " + col.name);
                    continue;
                }
 */
                if (col.GetComponent<MineSpawner>() != null)
                {
                    Debug.Log("Ignored TheBigSphere: " + col.name);
                    continue;
                }
                Debug.Log("BLOCKED by: " + col.name);
                blocked = true;
                break;
            }

            if (blocked)
                return false;

            Quaternion rot =
                Quaternion.FromToRotation(Vector3.up, hit.normal);

            Instantiate(
                minePrefab,
                spawnPos + offset,
                rot
            );

            Debug.Log("Mine instantiated at: " + spawnPos);

            return true;
        }

        Debug.LogWarning("Ray MISS - no HexTile hit");
        return false;
    }
}