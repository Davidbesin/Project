using UnityEngine;
using System.IO;

[RequireComponent(typeof(Mine))]
public class MineSave : MonoBehaviour
{
    private Mine mine;
    private MineLevel mineLevel;
    [SerializeField] string typeOf;
    private bool subscribed = false; // gate flag

    private void Awake()
    {
        mine = GetComponent<Mine>();
        mineLevel = GetComponent<MineLevel>();
    }

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
            SaveManager.Instance.AllSave -= SaveMine;
            subscribed = false;
        }
    }

    private void TrySubscribe()
    {
        if (!subscribed && SaveManager.Instance != null)
        {
            SaveManager.Instance.AllSave += SaveMine;
            subscribed = true;
        }
        else if (SaveManager.Instance == null)
        {
            Debug.LogError("SaveManager not ready when MineSave enabled!");
        }
    }

    public void SaveMine()
    {
        if (mine == null) return;

        MineSaveData data = new MineSaveData(mine, mineLevel, typeOf);
        string json = JsonUtility.ToJson(data, true);

        // Safer file name: use instanceID instead of position
        string safeName = $"Mine_{gameObject.GetInstanceID()}.json";
        string path = Path.Combine(Application.persistentDataPath, safeName);

        File.WriteAllText(path, json);
        Debug.Log($"Mine saved: {path}");
    }
}
