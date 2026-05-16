using UnityEngine;

public class LevelProgression : MonoBehaviour
{
  
    [Header("Enemy Tracking")]
    public int currentEnemyCount;

    [Header("Level State")]
    public int currentLevel;

    private void Update()
    {
        // Example condition: when enemy count reaches 10, go to level 2
        if (currentEnemyCount == 10 && currentLevel == 1)
        {
            AdvanceLevel(2);
        }

        // Another condition: when enemy count reaches 20, go to level 3
        if (currentEnemyCount == 20 && currentLevel == 2)
        {
            AdvanceLevel(3);
        }
    }

    private void AdvanceLevel(int nextLevel)
    {
        currentLevel = nextLevel;
        Debug.Log("Level advanced to: " + currentLevel);

        // Here you can trigger whatever happens when level changes:
        // - spawn stronger enemies
        // - change environment
        // - update UI
    }
}
