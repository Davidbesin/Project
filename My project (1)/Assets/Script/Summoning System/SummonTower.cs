using UnityEngine;

public class SummonTower : MonoBehaviour
{
    [SerializeField]GameObject attackTower;
    [SerializeField]GameObject aoETower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SummonAttackTower()
    {
        if (!SummonManager.Instance.NextAllowed) return;
        if (TowerTokenTransaction.towerTokenList.Count > 0)
        {
            TowerTokenTransaction.towerTokenList.RemoveAt(0); 
            Instantiate(attackTower, TowerManager.Instance.transform);
            SummonManager.Instance.AllowedStatus(false);
        }     
    }

    public void SummonAoETower()
    {
        if (!SummonManager.Instance.NextAllowed) return;
        if (TowerTokenTransaction.towerTokenList.Count > 0)
        {
            TowerTokenTransaction.towerTokenList.RemoveAt(0); 
            Instantiate(aoETower, TowerManager.Instance.transform);
            SummonManager.Instance.AllowedStatus(false);
        }  
    }   
}
