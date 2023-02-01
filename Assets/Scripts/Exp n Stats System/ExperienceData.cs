using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats/Experience_Data", order = 1)]
public class ExperienceData : ScriptableObject
{
    public LevelMilestones[] milestones = new LevelMilestones[5];

    public int CheckLevel(int xp_Val)
    {
        for (int i = 0; i < milestones.Length; i++)
        {
            if (xp_Val >= milestones[i].xp_Val)
                return i;
        }

        return 0;
    }
}

public class LevelMilestones
{
    public int xp_Val;
}