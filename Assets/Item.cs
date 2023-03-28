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

    void Awake()
    {
        timer = 10;     
    }

    public abstract void Effect(PlayerStats stats);

    public abstract void Update();
}
