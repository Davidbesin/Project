using UnityEngine;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }
    public List<BaseDefensiveTower> MyTowers = new();

    public int TotalWaveStrength { get; private set; }

    void Awake()
    {
        Instance = this;
    }

  //  void Start() => RecalculateWaveStrength();

    public void JoinList(BaseDefensiveTower tower)
    {
        MyTowers.Add(tower);
        RecalculateWaveStrength();
    }

    public void GetOutOfList(BaseDefensiveTower tower)
    {
        MyTowers.Remove(tower);
        RecalculateWaveStrength();
    }

    public void RecalculateWaveStrength()
    {
        TotalWaveStrength = 0;
        foreach (var tower in MyTowers)
        {
            TotalWaveStrength += tower.ContributeToWave();
        }
    }
}
