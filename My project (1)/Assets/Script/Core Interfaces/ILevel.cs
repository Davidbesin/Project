using UnityEngine;

public interface ILevel
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int Level{get;}
    void IncreaseLevel();

    void ResetLevel();

    string LevelText{get;}
}
