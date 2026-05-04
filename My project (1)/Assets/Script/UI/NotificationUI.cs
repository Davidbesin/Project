using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class NotificationUI : MonoBehaviour
{
    public static NotificationUI Instance;

    public TextMeshProUGUI text;
    public float displayTime = 2f;

    Queue<string> queue = new Queue<string>();
    bool isShowing;

    void Awake()
    {
        Instance = this;
        text.gameObject.SetActive(false);
    }

    public void Show(string message)
    {
        queue.Enqueue(message);

        if (!isShowing)
            StartCoroutine(ProcessQueue());
    }

    IEnumerator ProcessQueue()
    {
        isShowing = true;

        while (queue.Count > 0)
        {
            string msg = queue.Dequeue();

            text.text = msg;
            text.gameObject.SetActive(true);

            yield return new WaitForSeconds(displayTime);

            text.gameObject.SetActive(false);
        }

        isShowing = false;
    }
}