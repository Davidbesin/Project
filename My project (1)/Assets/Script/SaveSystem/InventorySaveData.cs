using UnityEngine;

[System.Serializable]
public class InventorySaveData
{

    public int regulator1Level;
    public int regulator2Level;

    public InventorySaveData( UpgradeableStatInterface upgrade1, UpgradeableStatInterface upgrade2)
    {

        // Save upgrade levels
        regulator1Level = (upgrade1 != null) ? upgrade1.level : 1;
        regulator2Level = (upgrade2 != null) ? upgrade2.level : 1;
    }
}
