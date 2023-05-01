using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelSettings : ScriptableObject
{
    
    public LevelData[] levels;
    LevelData SelectedLevel;

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    Difficulty selectedDifficulty;

    public void SetSelectedLevel(int level, Difficulty difficulty)
    {
        SelectedLevel = levels[level];
        selectedDifficulty = difficulty;
    }
    public LevelData GetSelectedLevel()
    {
        return SelectedLevel;
    }
    public Difficulty GetDifficulty()
    {
        return selectedDifficulty;
    }
}

[System.Serializable]
public class LevelData
{
    public string Name;

    [Range(0, 100)]
    public float initialTankChance = 0;
    [Range(0, 100)]
    public float additionalTankChance = 0;

    public EnemyData[] enemyData;

    public int Level;
    public GameObject EasyLevelGen;
    public GameObject MediumLevelGen;
    public GameObject HardLevelGen;


}

[System.Serializable]
public class EnemyData
{
    public EnemyInfo.Type Type;
    public EnemyStatRange StatRange;
}
