using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour, IGridManager
{
    public List<Tile> boughtTiles = new();
    public static IGridManager Instance { get; private set; }

    void Awake() => Instance = this;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void JoinList(Tile tile)
    {
        boughtTiles.Add(tile);  
    }

    public void RemoveList(Tile tile)
    {
        boughtTiles.Remove(tile);
    }
}

public interface IGridManager 
{
    void JoinList(Tile tile);
    void RemoveList(Tile tile);
}
