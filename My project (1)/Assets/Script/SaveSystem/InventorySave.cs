using UnityEngine;
using System.IO;

public class InventorySave : MonoBehaviour
{
    [SerializeField] private InventoryRegulator regulator1;
    [SerializeField] private InventoryRegulator regulator2;

    private string SavePath => Path.Combine(Application.persistentDataPath, "InventoryRegulators.json");

    private void OnEnable()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllSave += SaveBoth;
        else
            Debug.LogError("SaveManager not ready when InventoryRegulatorsSave enabled!");
    }

    private void OnDisable()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllSave -= SaveBoth;
    }

    public void SaveBoth()
    {
        if (regulator1 == null || regulator2 == null)
        {
            Debug.LogError("One or both regulators not assigned!");
            return;
        }

        InventorySaveData data = new InventorySaveData(regulator1, regulator2);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);

        Debug.Log($"Inventory regulators saved: {SavePath}");
    }
}
