using UnityEngine;

[System.Serializable]
public class ResourceSaveData
{
  public string type;
    public int amount;

    public ResourceSaveData(BaseResource resource, string type)
    {
        this.type = type;
        amount = resource.Amount;
    }
}
