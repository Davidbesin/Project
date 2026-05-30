using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public enum Stage
    {
        WallStage1,
        WallStage2,
        WallStage3,
        WallStage4
    }

    public Stage currentStage = Stage.WallStage1;

    [Header("Stage 1")]
    [SerializeField] GameObject[] walls1 = new GameObject[3];
    [SerializeField] GameObject stage1Object;

    [Header("Stage 2")]
    [SerializeField] GameObject[] walls2 = new GameObject[3];
    [SerializeField] GameObject stage2Object;

    [Header("Stage 3")]
    [SerializeField] GameObject[] walls3 = new GameObject[3];
    [SerializeField] GameObject stage3Object;

    [Header("Stage 4")]
    [SerializeField] GameObject[] walls4 = new GameObject[3];
    [SerializeField] GameObject stage4Object;

    void Update()
    {
        stage1Object.SetActive(currentStage == Stage.WallStage1);
        stage2Object.SetActive(currentStage == Stage.WallStage2);
        stage3Object.SetActive(currentStage == Stage.WallStage3);
        stage4Object.SetActive(currentStage == Stage.WallStage4);
    }
}