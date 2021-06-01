using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public float FadeRate;
    private Image image;
    private float targetAlpha;

    void Start()
    {
        image = GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError("Error: No image on " + name);
        }
        Material instantiatedMaterial = Instantiate<Material>(image.material);
        image.material = instantiatedMaterial;
        targetAlpha = image.material.color.a;
    }
    
    void Update()
    {
        Color curColor = image.material.color;
        float alphaDiff = Mathf.Abs(curColor.a - targetAlpha);
        if (alphaDiff > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, FadeRate * Time.deltaTime);
            image.material.color = curColor;
        }
    }

    public void FadeOut()
    {
        targetAlpha = 0.0f;
    }

    public void FadeIn()
    {
        targetAlpha = 1.0f;
    }
}
