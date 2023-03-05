using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats/Experience_Data", order = 1)]
public class ExperienceData : ScriptableObject
{
    public int[] milestones = new int[5];

    public int CheckLevel(int xp_Val)
    {
        for (int i = milestones.Length-1; i >= 0; i--)
        {
            if (xp_Val >= milestones[i])
                return i+1;
        }

        return 0;
    }
}