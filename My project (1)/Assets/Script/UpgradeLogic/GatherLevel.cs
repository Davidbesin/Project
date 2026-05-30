using UnityEngine;

public class GatherLevel : MonoBehaviour
{
    [SerializeField] private UpgradeableStatInterface upgradeableStat;

    public static int gatherLevel;

    private void Update()
    {
        if (upgradeableStat == null) return;

        gatherLevel = upgradeableStat.level;
    }
}