using UnityEngine;
using System.IO;

[RequireComponent(typeof(InventoryRegulator))]
public class InventorySave : MonoBehaviour
{
    [SerializeField] string typeOf; // optional type tag
    [SerializeField] private InventoryRegulator regulator1;
    [SerializeField] private InventoryRegulator regulator2;
    [SerializeField] private UpgradeableStatInterface upgrade1;
    [SerializeField]private UpgradeableStatInterface upgrade2;

    private bool subscribed = false;

   

    private void Start()
    {
        TrySubscribe();
    }

    private void OnEnable()
    {
        TrySubscribe();
    }

    private void OnDisable()
    {
        if (subscribed && SaveManager.Instance != null)
        {
            SaveManager.Instance.AllSave -= SaveInventory;
            subscribed = false;
        }
    }

    private void TrySubscribe()
    {
        if (!subscribed && SaveManager.Instance != null)
        {
            SaveManager.Instance.AllSave += SaveInventory;
            subscribed = true;
        }
        else if (SaveManager.Instance == null)
        {
            Debug.LogError("SaveManager not ready when InventorySave enabled!");
        }
    }

    public void SaveInventory()
    {
        if (regulator1 == null || regulator2 == null) return;

        InventorySaveData data = new InventorySaveData(upgrade1, upgrade2);
        string json = JsonUtility.ToJson(data, true);

        string safeName = $"Inventory_{gameObject.GetInstanceID()}.json";
        string path = Path.Combine(Application.persistentDataPath, safeName);

        File.WriteAllText(path, json);
        Debug.Log($"Inventory saved: {path}");
    }
}
