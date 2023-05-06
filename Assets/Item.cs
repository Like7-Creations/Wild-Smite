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

    public AudioSource itemAudio;

    public SFXClip item_CollectSFX;
    public SFXClip item_UseSFX;

    void Awake()
    {
        timer = 10;     
    }

    public abstract void Effect(PlayerStats stats);

    public abstract void Update();

    public void Play_CollectItemSFX()
    {
        itemAudio.PlayOneShot(item_CollectSFX.clip, item_CollectSFX.voumeVal);
    }

    public void Play_UseItemSFX()
    {
        itemAudio.PlayOneShot(item_UseSFX.clip, item_UseSFX.voumeVal);
    }
}
