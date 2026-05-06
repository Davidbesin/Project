using UnityEngine;
using TMPro;
using System.Collections;

public class TextMeshProUUpgradepdate : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    // Instead of BaseResource, reference UpgradeableStatInterface
    public UpgradeableStatInterface statInterface;

    // Pull the string from the stat’s level
    public string stringToUpdate => statInterface.NumberAsString;

    void OnEnable()
    {
        textMesh = GetComponent<TextMeshProUGUI>();

        if (textMesh == null)
        {
            Debug.LogError("No TextMeshProUGUI component found on this GameObject!");
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
