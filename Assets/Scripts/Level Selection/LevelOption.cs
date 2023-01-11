using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class LevelOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public int playerLevel = 10;
    public bool OptionCreated;

    enum difficulty { easy, medium, hard };

    int levelField;
    difficulty difficultyField;

    Image icon;
    TMP_Text ui;

    public Color defaultColor, HighlightColor;
    public float defaultScale, HighlightScale;

    private void Awake()
    {
        if (ui == null)
            ui = GetComponentInChildren<TMP_Text>();
        if (icon == null)
            icon = GetComponentInChildren<Image>();

        if (!OptionCreated)
        {
            levelField = Random.Range(1, playerLevel);
            difficultyField = (difficulty)Random.Range(0, 3);

            switch (difficultyField)
            {
                case difficulty.easy:
                    ui.text = "" + levelField + "- /";
                    break;
                case difficulty.medium:
                    ui.text = "" + levelField + "- //";
                    break;
                case difficulty.hard:
                    ui.text = "" + levelField + "- ///";
                    break;
            }

            OptionCreated = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        icon.color = HighlightColor;
        ui.color = HighlightColor;

        icon.transform.localScale = Vector3.one * HighlightScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        icon.color = defaultColor;
        ui.color = defaultColor;

        icon.transform.localScale = Vector3.one * defaultScale;
    }
}
