using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfiguration : MonoBehaviour
{
    public PlayerInput pInput { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public Material PlayerMaterial { get; set; }

    public PlayerConfiguration(PlayerInput pi)
    {
        //PlayerIndex = pi.
    }

}
