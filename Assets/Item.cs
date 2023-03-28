using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;

public abstract class Item : MonoBehaviour
{
    public bool useItem;

    public float timer;
    public float duration;

    public int buffAmount;
    public int originalAmount;

    public Sprite itemUI;

    public abstract void Effect(PlayerStats stats);

    public abstract void Update();
}
