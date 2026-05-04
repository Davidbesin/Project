using UnityEngine;
using System.Collections.Generic;

public class SummonWithToken : MonoBehaviour
{
    [SerializeField] List<GameObject> initialTowerRed = new();
    [SerializeField] List<GameObject> initialTowerGreen = new();
    [SerializeField] List<GameObject> initialTower = new();
    [SerializeField] Vector3 offset;

    public TowerTokenTransaction tokenTransaction;

    public enum TowerType { AoETower, AttackTower, MoveSlowlyTower }

    public void ChooseAoETower()
    {
        if (!SummonInitialTowerWithToken()) return;
        Instantiate(initialTowerRed[0]);
        Instantiate(initialTowerGreen[0]);
        Instantiate(initialTower[0], transform.position - offset, Quaternion.identity);
    }

    public void ChooseAttackTower()
    {
        if (!SummonInitialTowerWithToken()) return;
        Instantiate(initialTowerRed[1]);
        Instantiate(initialTowerGreen[1]);
        Instantiate(initialTower[1], transform.position - offset, Quaternion.identity);
    }

    public void ChooseMoveSlowlyTower()
    {
        if (!SummonInitialTowerWithToken()) return;
        Instantiate(initialTowerRed[2]);
        Instantiate(initialTowerGreen[2]);
        Instantiate(initialTower[2], transform.position - offset, Quaternion.identity);
    }

    private bool SummonInitialTowerWithToken()
    {
        if (tokenTransaction.towerTokenList.Count <= 0) return false;
        tokenTransaction.towerTokenList.RemoveAt(0);
        return true;
    }
}
