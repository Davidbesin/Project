using UnityEngine;

public class UiTriggerBinder : MonoBehaviour
{
    [Tooltip("Tag of the UI element that has UiClickTrigger")]
    [SerializeField] private string uiTag = "UpgradeButton";

    [Tooltip("Reference to the stat interface this binder should connect")]
    [SerializeField] private UpgradeableStatInterface statInterface;

    private UiClickTrigger uiClickTrigger;

    private void Awake()
    {
        // Find the UI GameObject by tag
        GameObject uiObject = GameObject.FindGameObjectWithTag(uiTag);
        if (uiObject == null)
        {
            Debug.LogError($"No GameObject found with tag {uiTag}");
            return;
        }

        // Get the UiClickTrigger component
        uiClickTrigger = uiObject.GetComponent<UiClickTrigger>();
        if (uiClickTrigger == null)
        {
            Debug.LogError("UiClickTrigger component not found on tagged UI object!");
            return;
        }
    }

    private void OnEnable()
    {
        if (uiClickTrigger != null && statInterface != null)
        {
            uiClickTrigger.OnUpgradeAction.AddListener(statInterface.UpgradeLevel);
            Debug.Log("Subscribed UpgradeLevel to UI click trigger.");
        }
    }

    private void OnDisable()
    {
        if (uiClickTrigger != null && statInterface != null)
        {
            uiClickTrigger.OnUpgradeAction.RemoveListener(statInterface.UpgradeLevel);
            Debug.Log("Unsubscribed UpgradeLevel from UI click trigger.");
        }
    }
}
