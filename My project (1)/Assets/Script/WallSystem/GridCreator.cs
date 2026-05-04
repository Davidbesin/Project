using UnityEngine;

[RequireComponent(typeof(Grid))] 
public class GridCreator : MonoBehaviour
{
    public GameObject cubePrefab; // Drag a cube prefab here
    public int width = 10;
    public int height = 10;

    private Grid grid;

    void Start()
    {
        grid = GetComponent<Grid>();
        GenerateGrid();
    }

    private void Update()
    {
        
    }
    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                // Convert grid coordinates (0,1,2...) to world positions
                Vector3Int cellPosition = new Vector3Int(x, 0, z);
                Vector3 worldPosition = grid.CellToWorld(cellPosition);

                // Add an offset if you want cubes centered on the cell
                Vector3 centerOffset = grid.cellSize / 2;
                
                // Spawn the cube
                Instantiate(cubePrefab, worldPosition + centerOffset, Quaternion.identity, transform);
            }
        }
    }
}
