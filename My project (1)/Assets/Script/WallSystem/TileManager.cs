using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public List<Tile> tiles = new();

    void Awake()
    {
        // Find all Tile components in children
        tiles = new List<Tile>(GetComponentsInChildren<Tile>());

        // Assign IDs based on their index in the list
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].iD = i+1;
        }
    }

    void Start()
    {
        // Example: set tile with ID 210 as owned (if it exists)
        if (tiles.Count > 210)
        {
            tiles[210].SetOwned(true);
        }
    }
}
