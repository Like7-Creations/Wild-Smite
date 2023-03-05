using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatBar : MonoBehaviour
{

    [Header("Bars")]
    public Transform root;
    public GameObject barPrefab;
    public int maxBarValue;

    [Header("Stat Values")]
    public PlayerStat_Data data;
    public TMP_Text statText;

    int statValue;
    public Stat barStat;
    public enum Stat
    {
        HP,
        STAM,
        MATK,
        RATK
    }

    public void SetBars(PlayerStat_Data p)
    {
        data = p;

        switch (barStat)
        {
            case Stat.HP:
                statValue = data.hp;
                break;
            case Stat.STAM:
                statValue = data.stamina;
                break;
            case Stat.MATK:
                statValue = data.m_ATK;
                break;
            case Stat.RATK:
                statValue = data.r_ATK;
                break;
        }

        statText.text = "" + statValue;

        int barCount = Mathf.CeilToInt(statValue / maxBarValue);
        for (int i = 0; i <= barCount; i++)
        {
            Instantiate(barPrefab, root);
        }

    }

    public void ResetBar()
    {
        if (root.transform.childCount > 0)
            for (int j = root.transform.childCount - 1; j > 0; j--)
                Destroy(root.transform.GetChild(j).gameObject);
    }
}
