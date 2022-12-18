using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameMode : ScriptableObject
{
    public enum Gamemode
    {
        SinglePlayer,
        CoopPlayer
    }
   public Gamemode GM;

    public int PlayerOneChar;
    public int PlayerTwoChar;
}
