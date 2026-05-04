using UnityEngine;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
    public int iD;
    public bool Owned { get; private set; }
    public List<Trigger> triggers = new();
    public List<Wall> walls = new();

    void Awake()
    {
        // Automatically find all Trigger components in children
        triggers.AddRange(GetComponentsInChildren<Trigger>());
        
        // Optionally, also auto-populate walls if needed
        
    }

    public void SetOwned(bool setter)
    {
        Owned = setter;
        WallsControl(setter);

        foreach (var trigger in triggers)
        {
            trigger.SetBool(setter);
        }

        if (setter)
        {
            GridManager.Instance?.JoinList(this);
        }
        else
        {
            GridManager.Instance?.RemoveList(this);
        }
    }

    private void WallsControl(bool owned)
    {
        foreach (var wall in walls)
        {
            wall.gameObject.SetActive(owned);
        }
    }
}   