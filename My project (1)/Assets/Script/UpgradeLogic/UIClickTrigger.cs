using UnityEngine;
using System;
using UnityEngine.Events;

public class UiClickTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent OnUpgradeAction;

    // Called by the UI button
    public void UpgradeButtonClick()
    {
        OnUpgradeAction?.Invoke();
    }
}

