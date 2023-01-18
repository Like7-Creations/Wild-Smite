using UnityEngine;

public class MaterialColorChange : MonoBehaviour
{

    MeshRenderer cubeMeshRenderer;

    [SerializeField] [Range(0f, 1f)] float lerpTime;

    [SerializeField] public Color[] myColor;

    public int colorIndex = 0;

    float t = 0;

    int len;


    void Start()
    {
        cubeMeshRenderer = GetComponent<MeshRenderer>();
        len = myColor.Length;
    }


    void Update()
    {
        // MeshRenderer mr = GetComponent<MeshRenderer>();
        // Color col = mr.material.color;
        // col.a = 180; // pass float value here
        // mr.material.color = col;

        // RenderSettings.skybox.SetColor("_Tint", myColor[colorIndex]);

        cubeMeshRenderer.material.color = Color.Lerp(cubeMeshRenderer.material.color, myColor[colorIndex], lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= len) ? 0 : colorIndex;
        }
    }
}
