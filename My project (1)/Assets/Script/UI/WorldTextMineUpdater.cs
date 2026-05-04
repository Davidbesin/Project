using UnityEngine;
using TMPro;
using System.Collections;

public class WorldTextMineUpdater : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI textMesh; 
    [SerializeField] private MineLevel level; // drag the MineLevel component here in Inspector

    public string stringToUpdate => level.LevelText;

    void OnEnable()
    {
        textMesh = GetComponent<TextMeshProUGUI>();

        if (textMesh == null)
        {
            Debug.LogError("No TextMeshPro (world-space) component found on this GameObject!");
            return;
        }

        StartCoroutine(UpdateTextCoroutine());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator UpdateTextCoroutine()
    {
        while (true)
        {
            textMesh.text = stringToUpdate;

            yield return new WaitForSeconds(1f);
        }
    }
}
