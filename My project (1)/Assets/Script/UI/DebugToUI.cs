using UnityEngine;

public class DebugToUI : MonoBehaviour
{
    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Log && NotificationUI.Instance != null)
        {
            NotificationUI.Instance.Show(logString);
        }
    }
}