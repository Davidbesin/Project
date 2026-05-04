using UnityEngine;
using TMPro;
using System.Collections;

public class NotificationUI : MonoBehaviour
{
    public static NotificationUI Instance;

    [Header("UI Reference")]
    public TextMeshProUGUI text;

    [Header("Settings")]
    public float displayTime = 2f;

    void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (text != null)
            text.gameObject.SetActive(false);
    }

    public void Show(string message)
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine(message));
    }

    IEnumerator ShowRoutine(string message)
    {
        text.text = message;
        text.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayTime);

        text.gameObject.SetActive(false);
    }
}