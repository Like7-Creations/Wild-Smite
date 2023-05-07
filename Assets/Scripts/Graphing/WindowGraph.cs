using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] Sprite circleSprite;
    RectTransform graphContainer;

    private void Awake()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
    }

    void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gObject = new GameObject("circle", typeof(Image));
        gObject.transform.SetParent(graphContainer, false);
        gObject.GetComponent<Image>().sprite = circleSprite;

        RectTransform rectTransform = gObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.zero;
    }

    public void ShowGraph(List<int> values)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        float yMax = 100;
        float xSize = graphWidth / values.Count;
        for (int i = 0; i < values.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (values[i] / yMax) * graphHeight;
            CreateCircle(new Vector2(xPosition, yPosition));
        }
    }
}
