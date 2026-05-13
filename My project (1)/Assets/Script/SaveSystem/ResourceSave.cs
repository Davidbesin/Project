using UnityEngine;
using System.IO;

public class ResourceSave : MonoBehaviour
{
    [SerializeField] private BaseResource resource;
    [SerializeField] private string type; // assign a unique key in Inspector

    private string SavePath => Path.Combine(Application.persistentDataPath, $"Resource_{type}.json");

    private void OnEnable()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllSave += SaveResource;
        else
            Debug.LogError("SaveManager not ready when ResourceSave enabled!");
    }

    private void OnDisable()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.AllSave -= SaveResource;
    }

    public void SaveResource()
    {
        if (resource == null)
        {
            Debug.LogError("No resource assigned to save!");
            return;
        }

        ResourceSaveData data = new ResourceSaveData(resource, type);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);

        Debug.Log($"Resource '{type}' saved: {SavePath}");
    }
}
