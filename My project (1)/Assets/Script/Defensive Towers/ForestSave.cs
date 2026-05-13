using UnityEngine;
using System.IO;

[RequireComponent(typeof(Transform))]
public class ForestSave : MonoBehaviour
{
    private bool subscribed = false;
    private string SavePath => Path.Combine(Application.persistentDataPath, $"Forest_{gameObject.GetInstanceID()}.json");

    private void OnEnable()
    {
        TrySubscribe();
    }

    private void Start()
    {
        TrySubscribe();
    }

    private void OnDisable()
    {
        if (subscribed && SaveManager.Instance != null)
        {
            SaveManager.Instance.AllSave -= Save;
            subscribed = false;
        }
    }

    private void TrySubscribe()
    {
        if (!subscribed && SaveManager.Instance != null)
        {
            SaveManager.Instance.AllSave += Save;
            subscribed = true;
        }
        else if (SaveManager.Instance == null)
        {
            Debug.LogError("SaveManager not ready when ForestSave enabled!");
        }
    }

    public void Save()
    {
        ForestSaveData data = new ForestSaveData(transform);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log($"Forest object saved: {SavePath}");
    }

    public void Load()
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("No save file found!");
            return;
        }

        string json = File.ReadAllText(SavePath);
        ForestSaveData data = JsonUtility.FromJson<ForestSaveData>(json);

        transform.position = data.position;
        transform.rotation = data.rotation;
        transform.localScale = data.scale;

        Debug.Log("Forest object loaded from save.");
    }
}
