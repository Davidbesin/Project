using UnityEngine;

public class SpeedLevel : MonoBehaviour
{
    [SerializeField] private UpgradeableStatInterface upgradeableStat;

    public static int speedLevel;

    private void Update()
    {
        if (upgradeableStat == null) return;

        speedLevel = upgradeableStat.level;
    }
}