using UnityEngine;

[System.Serializable]
public class InventorySaveData
{
    public int regulator1MaxAllowedSum;
    public int regulator1CurrentSum;

    public int regulator2MaxAllowedSum;
    public int regulator2CurrentSum;

    public InventorySaveData(InventoryRegulator regulator1, InventoryRegulator regulator2)
    {
        regulator1MaxAllowedSum = regulator1.MaxAllowedSum;
        regulator1CurrentSum    = regulator1.Sum;

        regulator2MaxAllowedSum = regulator2.MaxAllowedSum;
        regulator2CurrentSum    = regulator2.Sum;
    }
}
