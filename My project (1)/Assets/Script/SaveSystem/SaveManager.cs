using UnityEngine;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    // One central event
    public event Action AllSave;

    private void Awake()
    {
        Instance = this;
    }

    // Hook this to a UI button
    public void SaveAll()
    {
        AllSave?.Invoke();
        Debug.Log("All objects saved.");
    }
}
